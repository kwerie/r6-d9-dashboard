import {Component} from '@angular/core';
import {Observable} from "rxjs";
import {User} from "../../value-objects/user.value-object";
import {UserService} from "../../services/user/user.service";
import {environment} from "../../../environments/environment";
import {LoginSessionService} from "../../services/login-session/login-session.service";

@Component({
    selector: 'app-dashboard-page-layout',
    templateUrl: './dashboard-page-layout.component.html',
    styleUrl: './dashboard-page-layout.component.scss'
})
export class DashboardPageLayoutComponent {

    public readonly user: Observable<User>;
    public discordOAuthUrl: string = environment.discordOAuthUrl;

    public constructor(
        private readonly userService: UserService,
        private readonly loginSessionService: LoginSessionService,
    ) {
        this.user = this.userService.getUser();
    }

    public logout() {
        this.loginSessionService.clear();
        window.location.reload();
    }
}
