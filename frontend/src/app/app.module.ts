import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";

import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from "./app.component";
import {HomePageComponent} from "./pages/home-page/home-page.component";
import {HomePageLayoutComponent} from "./layouts/home-page-layout/home-page-layout.component";
import {DiscordCallbackPageComponent} from "./pages/auth/discord-callback-page/discord-callback-page.component";
import {LoginWithDiscordButtonComponent} from "./components/login-with-discord-button/login-with-discord-button.component";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {DashboardPageComponent} from './pages/dashboard-page/dashboard-page.component';
import {DashboardPageLayoutComponent} from './layouts/dashboard-page-layout/dashboard-page-layout.component';
import {LocalStorageService} from "./services/local-storage/local-storage.service";
import {AuthService} from "./services/auth/auth.service";
import {LoginSessionService} from "./services/login-session/login-session.service";
import {UserService} from "./services/user/user.service";
import {AuthenticationInterceptor} from "./interceptors/authentication.interceptor";
import {jwtDecode} from "jwt-decode";
import {JWT_DECODE} from "./constants/jwt-decode.constant";
import {JwtService} from "./services/jwt/jwt.service";
import { TopNavigationComponent } from './components/top-navigation/top-navigation.component';

function jwtDecodeFactory(): unknown {
    return jwtDecode;
}

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        HomePageLayoutComponent,
        DiscordCallbackPageComponent,
        LoginWithDiscordButtonComponent,
        DashboardPageComponent,
        DashboardPageLayoutComponent,
        TopNavigationComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FontAwesomeModule,
        HttpClientModule,
    ],
    providers: [
        LocalStorageService,
        AuthService,
        LoginSessionService,
        JwtService,
        UserService,
        {
            provide: JWT_DECODE,
            useFactory: jwtDecodeFactory
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthenticationInterceptor,
            multi: true
        }
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
