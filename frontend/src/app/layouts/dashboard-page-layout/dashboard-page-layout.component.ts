import {Component, EventEmitter, Input, Output} from '@angular/core';
import {User} from "../../value-objects/user.value-object";

@Component({
    selector: 'app-dashboard-page-layout',
    templateUrl: './dashboard-page-layout.component.html',
    styleUrl: './dashboard-page-layout.component.scss'
})
export class DashboardPageLayoutComponent {
    @Input()
    public user: User;
    
    @Input()
    public discordOAuthUrl: string;

    @Output()
    public logoutEmitter: EventEmitter<void> = new EventEmitter();
}
