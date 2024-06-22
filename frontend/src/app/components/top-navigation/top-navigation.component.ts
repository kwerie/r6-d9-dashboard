import {Component, EventEmitter, Input, Output} from '@angular/core';
import {User} from "../../value-objects/user.value-object";
import {NavigationItem} from "../../interfaces/navigation-item.interface";

@Component({
    selector: 'app-top-navigation',
    templateUrl: './top-navigation.component.html',
    styleUrl: './top-navigation.component.scss'
})
export class TopNavigationComponent {
    @Input()
    public user?: User | null;

    @Input()
    public discordOAuthUrl?: string;

    @Output()
    public logoutEmitter: EventEmitter<void> = new EventEmitter();

    public navigationItems: NavigationItem[] = [
        {
            title: "Home",
            url: "",
        },
        {
            title: "Features",
            url: "/features"
        },
    ];
    protected readonly alert = alert;
}
