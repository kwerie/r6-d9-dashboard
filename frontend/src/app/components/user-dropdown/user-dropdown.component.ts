import {Component, ElementRef, EventEmitter, HostListener, Input, Output, ViewChild} from '@angular/core';
import {User} from "../../value-objects/user.value-object";
import {faChevronDown, faChevronUp} from "@fortawesome/free-solid-svg-icons";
import {Router} from "@angular/router";
import {ActionItem} from "../../interfaces/action-item.interface";

@Component({
    selector: 'app-user-dropdown',
    templateUrl: './user-dropdown.component.html',
    styleUrl: './user-dropdown.component.scss'
})
export class UserDropdownComponent {
    @Input()
    public user: User;

    @Output()
    public logoutEmitter: EventEmitter<void> = new EventEmitter();

    // Dropdown
    public dropdownActiveIcon = faChevronUp;
    public dropdownInactiveIcon = faChevronDown;
    public dropdownToggled = false;

    @ViewChild('dropdownContent')
    private readonly dropdownContent: ElementRef;

    @ViewChild('dropdownToggler')
    private readonly dropdownToggler: ElementRef;

    @HostListener('document:click', ['$event'])
    private click(event: MouseEvent): void {
        if (this.dropdownToggled
            && !this.dropdownContent.nativeElement.contains(event.target)
            && !this.dropdownToggler.nativeElement.contains(event.target)
        ) {
            this.toggleDropdown();
        }
    }

    public middleActions: ActionItem[] = [
        {
            title: "Dashboard",
            action: async () => {
                await this.router.navigate(["/dashboard"]);
            }
        }
    ];

    public bottomActions: ActionItem[] = [
        {
            title: "Support server",
            action: () => {
                console.log("join discord clicked")
            }
        },
        {
            title: "Logout",
            action: () => {
                this.logoutEmitter.emit();
            },
        }
    ];

    public constructor(
        private readonly router: Router
    ) {
    }

    public toggleDropdown() {
        this.dropdownToggled = !this.dropdownToggled;
    }
}
