import {Component} from '@angular/core';
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {User} from "../../value-objects/user.value-object";
import {LoginSessionService} from "../../services/login-session/login-session.service";
import {UserService} from "../../services/user/user.service";

@Component({
    selector: 'app-home-page-layout',
    templateUrl: './home-page-layout.component.html',
    styleUrl: './home-page-layout.component.scss'
})
export class HomePageLayoutComponent {
    public discordOAuthUrl: string = environment.discordOAuthUrl;
    public user: Observable<User|null>;


    public constructor(
        private readonly loginSessionService: LoginSessionService,
        private readonly userService: UserService,
    ) {
        this.loginSessionService.getLoginSession().subscribe({
            next: (session) => {
                if (session)
                {
                    this.user = this.userService.getUser();
                }
            }
        })
    }

    public logout() {
        this.loginSessionService.clear();
        window.location.reload();
    }
}
