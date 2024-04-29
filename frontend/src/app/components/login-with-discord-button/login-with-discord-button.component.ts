import { Component, Input } from "@angular/core";
import { faDiscord } from "@fortawesome/free-brands-svg-icons";

@Component({
    selector: "app-login-with-discord-button",
    templateUrl: "./login-with-discord-button.component.html",
    styleUrl: "./login-with-discord-button.component.scss",
})
export class LoginWithDiscordButtonComponent {
    @Input()
    public discordOAuthUrl?: string;

    public discordIcon = faDiscord;
}
