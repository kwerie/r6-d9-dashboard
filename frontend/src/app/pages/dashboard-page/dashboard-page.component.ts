import {Component} from '@angular/core';
import {Observable} from "rxjs";
import {User} from "../../value-objects/user.value-object";
import {environment} from "../../../environments/environment";
import {UserService} from "../../services/user/user.service";
import {LoginSessionService} from "../../services/login-session/login-session.service";

@Component({
    selector: 'app-dashboard-page',
    templateUrl: './dashboard-page.component.html',
    styleUrl: './dashboard-page.component.scss'
})
export class DashboardPageComponent {
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
