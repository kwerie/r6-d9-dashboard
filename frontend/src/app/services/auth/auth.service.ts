import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class AuthService {
    private readonly authEndpoint = `${environment.baseApiUrl}/auth/discord/login`;
    public constructor(private readonly httpClient: HttpClient) {}

    public login(
        code: string
    ): Observable<any> /*TODO - create JSEndResponseInterface*/ {
        return this.httpClient.post(this.authEndpoint, {
            code,
        });
    }
}
