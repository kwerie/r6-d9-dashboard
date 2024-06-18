import {CanActivateChildFn, CanActivateFn, Router, UrlTree} from '@angular/router';
import {inject} from "@angular/core";
import {LoginSessionService} from "../services/login-session/login-session.service";

export function AuthenticationGuard(): CanActivateFn | CanActivateChildFn {
    return () => {
        let canActivate: boolean | UrlTree = false;
        const loginSessionService = inject(LoginSessionService);
        const router = inject(Router);

        loginSessionService.isAuthenticated().subscribe({
            next: value => {
                canActivate = value ? value : router.createUrlTree(["/"]);
            }
        })

        return canActivate;
    }
}
