import {Injectable} from '@angular/core';
import {EMPTY, Observable, ReplaySubject} from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class LocalStorageService {

    private storageMap: Map<string, ReplaySubject<string | null>> = new Map();

    public constructor() {
        this.initializeStorageMap(localStorage);
    }

    public createPersistentStorage(key: string, initialValue: string | null): void {
        this.addStorageItem(key, initialValue);
    }

    public setItem(key: string, value: string | null): void {
        if (!this.storageMap.has(key)) {
            this.createPersistentStorage(key, value);
        }

        if (value === null) {
            value = '';
        }

        const storageItem = this.storageMap.get(key);

        if (storageItem) {
            localStorage.setItem(key, value);
            storageItem.next(value);
        }
    }

    public getItem(key: string): Observable<string | null> {
        if (!this.storageMap.has(key)) {
            return EMPTY;
        }
        return this.storageMap.get(key)!.asObservable();
    }

    public removeItem(key: string): void {
        if (!this.storageMap.has(key)) {
            throw new Error(
                `Storage item with key: ${key} hasn't been created yet.`,
            );
        }
        const storageItem = this.storageMap.get(key);
        if (storageItem) {
            localStorage.removeItem(key);
            storageItem.next(null);
            storageItem.complete();
        }
    }

    private addStorageItem(
        key: string,
        initialValue: string | null,
    ): void {
        const alreadyExists = this.storageMap.has(key);

        if (
            !alreadyExists ||
            (alreadyExists && this.storageMap.get(key)?.closed)
        ) {
            const storageItem: ReplaySubject<string | null> = new ReplaySubject(1);
            this.storageMap.set(key, storageItem);
        }

        if (!alreadyExists) {
            this.setItem(key, initialValue);
        }
    }

    private initializeStorageMap(
        storage: Storage,
    ): void {
        for (let i = 0; i < storage.length; i++) {
            const storageKey = storage.key(i);
            if (!storageKey) {
                continue;
            }

            let storedValue: string | null;
            storedValue = localStorage.getItem(storageKey);

            if (
                !storedValue ||
                storedValue === '' ||
                storedValue === 'undefined' ||
                storedValue === 'null'
            ) {
                storedValue = null;
            }

            this.addStorageItem(storageKey, storedValue);
        }
    }
}
