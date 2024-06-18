import {Component, Input} from '@angular/core';
import {User} from "../../value-objects/user.value-object";

@Component({
    selector: 'app-top-navigation',
    templateUrl: './top-navigation.component.html',
    styleUrl: './top-navigation.component.scss'
})
export class TopNavigationComponent {
    @Input()
    public user!: User|null;
}
