import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { AuthService } from "../../../services/auth/auth.service";

@Component({
    selector: "app-discord-callback-page",
    templateUrl: "./discord-callback-page.component.html",
    styleUrl: "./discord-callback-page.component.scss",
})
export class DiscordCallbackPageComponent {
    private code: BehaviorSubject<string> = new BehaviorSubject<string>("");
    public constructor(
        private readonly route: ActivatedRoute,
        private readonly authService: AuthService
    ) {
        this.route.queryParams.subscribe({
            next: (values) => {
                if (values["code"]) {
                    this.code.next(values["code"]);
                }
            },
        });

        this.code.subscribe({
            next: (code) => {
                console.log("code", code);
                this.authService.login(code).subscribe({
                    next: (res) => {
                        console.log(res);
                    },
                });
            },
        });
    }
}
