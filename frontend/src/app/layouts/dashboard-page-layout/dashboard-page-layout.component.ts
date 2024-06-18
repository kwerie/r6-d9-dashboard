import {Component} from '@angular/core';
import {Observable} from "rxjs";
import {User} from "../../value-objects/user.value-object";
import {UserService} from "../../services/user/user.service";

@Component({
    selector: 'app-dashboard-page-layout',
    templateUrl: './dashboard-page-layout.component.html',
    styleUrl: './dashboard-page-layout.component.scss'
})
export class DashboardPageLayoutComponent {

    public readonly user: Observable<User>;

    public constructor(
        private readonly userService: UserService,
    ) {
        this.user = this.userService.getUser();
    }
}
