import {Component} from '@angular/core';
import {environment} from "../../../environments/environment";
import {faQuestionCircle} from "@fortawesome/free-regular-svg-icons";
import {faDiscord} from "@fortawesome/free-brands-svg-icons";
import {ButtonSize} from "../../components/button/button.component";

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrl: './home-page.component.scss',
})
export class HomePageComponent {
    public readonly discordOAuthUrl: string = environment.discordOAuthUrl;
    public readonly supportServerUrl: string = "https://discord.gg/Bxjm2UYkwR";

    protected readonly faQuestionCircle = faQuestionCircle;
    protected readonly faDiscord = faDiscord;
    protected readonly ButtonSize = ButtonSize;
}
