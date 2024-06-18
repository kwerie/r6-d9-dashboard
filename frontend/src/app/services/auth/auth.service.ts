import {Injectable} from "@angular/core";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {LoginSession} from "../../value-objects/login-session.value-object";
import {LoginSessionService} from "../login-session/login-session.service";

@Injectable({
    providedIn: "root",
})
export class AuthService {
    private readonly authEndpoint = `${environment.baseApiUrl}/auth/discord/login`;

    public constructor(
        private readonly httpClient: HttpClient,
        private readonly loginSessionService: LoginSessionService,
    ) {
    }

    public login(code: string): Observable<any> /*TODO - create JSEndResponseInterface*/ {
        return this.httpClient.post(this.authEndpoint, {
            code,
        }).pipe(
            tap((res: any) => {
                const loginSession = LoginSession.create(res);
                this.loginSessionService.setLoginSession(loginSession);
            }),
        );
    }
}
