import {Component, Input} from '@angular/core';
import {IconDefinition} from "@fortawesome/fontawesome-svg-core";

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrl: './button.component.scss'
})
export class ButtonComponent {
    @Input()
    public buttonUrl: string;

    @Input()
    public buttonIcon: IconDefinition;

    @Input()
    public buttonStyle: ButtonStyle = ButtonStyle.Primary;

    @Input()
    public buttonSize: ButtonSize = ButtonSize.Medium;
}

export enum ButtonStyle {
    Primary,
    Secondary,
}

export enum ButtonSize {
    Small = "small",
    Medium = "medium",
    Large = "large",
    XL = "xl"
}