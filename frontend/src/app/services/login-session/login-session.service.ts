import {Injectable} from '@angular/core';
import {LocalStorageService} from "../local-storage/local-storage.service";
import {LoginSession} from "../../value-objects/login-session.value-object";
import {filter, first, map, Observable, ReplaySubject, tap} from "rxjs";
import {JwtService} from "../jwt/jwt.service";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";

const LOGIN_SESSION_STORAGE_KEY = "login_session";

@Injectable({
    providedIn: 'root'
})
export class LoginSessionService {
    private readonly refreshTokenEndpoint: string = `${environment.baseApiUrl}/auth/refresh-token`;

    private loginSession = new ReplaySubject<LoginSession | null>(1);
    private access_token: string | null = null;
    private refresh_token: string | null = null;

    public constructor(
        private readonly localStorageService: LocalStorageService,
        private readonly jwtService: JwtService,
        private readonly httpClient: HttpClient,
    ) {
        this.localStorageService.createPersistentStorage(LOGIN_SESSION_STORAGE_KEY, "");
        this.localStorageService.getItem(LOGIN_SESSION_STORAGE_KEY)
            .pipe(
                first()
            ).subscribe({
            next: value => {
                const loginSession = value ? LoginSession.createFromJSON(value) : null;
                this.persist(loginSession);
                this.loginSession.next(loginSession);
            }
        })
    }

    public setLoginSession(loginSession: LoginSession | null): void {
        this.access_token = loginSession?.access_token ?? null;
        this.refresh_token = loginSession?.refresh_token ?? null;
        this.persist(loginSession);
        this.loginSession.next(loginSession);
    }

    public persist(loginSession: LoginSession | null): void {
        this.localStorageService.setItem(LOGIN_SESSION_STORAGE_KEY, loginSession ? JSON.stringify(loginSession) : null);
    }

    public clear(): void {
        return this.persist(null);
    }

    public isAuthenticated(): Observable<boolean> {
        return this.localStorageService.getItem(LOGIN_SESSION_STORAGE_KEY).pipe(
            map(loginSession => !!loginSession)
        );
    }

    public getLoginSession(): Observable<LoginSession | null> {
        return this.loginSession.asObservable();
    }

    public getAccessToken(): string | null {
        return this.access_token;
    }

    public getRefreshToken(): string | null {
        return this.refresh_token;
    }

    public getValidToken(): Observable<string> {
        // @ts-ignore - null cannot be returned due to it being filtered, compiler does not understand this.
        return this.getLoginSession()
            .pipe(
                map(loginSession => {
                    return {
                        accessToken: loginSession?.access_token ?? null,
                        refreshToken: loginSession?.refresh_token ?? null,
                    }
                }),
                map(token => {
                    if (!token.accessToken || !token.refreshToken) {
                        return null;
                    }
                    if (this.jwtService.isTokenExpired(token.accessToken)) {
                        this.refreshToken(token.refreshToken).subscribe();
                        return null;
                    }
                    return token.accessToken;
                }),
                filter(token => !!token),
            );
    }

    // TODO: move this to the authentication service 
    //  (LoginSessionService has to be decoupled into 2 services: LoginSessionService & LoginSessionStorageService)
    public refreshToken(refreshToken: string): Observable<any> {
        return this.httpClient.post(
            this.refreshTokenEndpoint,
            {
                "refreshToken": refreshToken
            }
        )
            .pipe(
                tap((res: any) => {
                    const loginSession = LoginSession.create(res);
                    this.setLoginSession(loginSession);
                })
            )
    }
}
