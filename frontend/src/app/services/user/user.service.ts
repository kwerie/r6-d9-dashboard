import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {User} from "../../value-objects/user.value-object";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    public constructor(
        private readonly httpClient: HttpClient,
    ) {
    }

    public getUser(): Observable<User> {
        return this.httpClient.get("/api/auth/user").pipe(
            map((value: any) => User.create(value))
        )
    }
}
