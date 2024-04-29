import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { HomePageLayoutComponent } from "./layouts/home-page-layout/home-page-layout.component";
import { DiscordCallbackPageComponent } from "./pages/auth/discord-callback-page/discord-callback-page.component";
import { LoginWithDiscordButtonComponent } from "./components/login-with-discord-button/login-with-discord-button.component";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        HomePageLayoutComponent,
        DiscordCallbackPageComponent,
        LoginWithDiscordButtonComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FontAwesomeModule,
        HttpClientModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
