import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable} from "@angular/core";
import {first, Observable, reduce, switchMap} from "rxjs";
import {LoginSessionService} from "../services/login-session/login-session.service";

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
    public constructor(
        private readonly loginSessionService: LoginSessionService
    ) {
    }

    public intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const anonymousUrls = [
            "/api/auth/discord/login",
            "/api/auth/refresh-token",
        ];

        if (anonymousUrls.some(url => req.url === url)) {
            return next.handle(req);
        }

        return this.loginSessionService.getValidToken()
            .pipe(
                first(),
                switchMap((t: string) => {
                    // @ts-ignore
                    const clonedRequest = req.clone({
                        setHeaders: {
                            Authorization: `Bearer ${t}`
                        }
                    })
                    return next.handle(clonedRequest);
                })
            )
    }
}
