import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {HomePageComponent} from "./pages/home-page/home-page.component";
import {DiscordCallbackPageComponent} from "./pages/auth/discord-callback-page/discord-callback-page.component";
import {DashboardPageComponent} from "./pages/dashboard-page/dashboard-page.component";
import {AuthenticationGuard} from "./guards/authentication.guard";
import {FeaturesPageComponent} from "./pages/features-page/features-page.component";

const routes: Routes = [
    {
        path: "",
        component: HomePageComponent,
    },
    {
        path: "features",
        component: FeaturesPageComponent
    },
    {
        path: "auth/discord",
        component: DiscordCallbackPageComponent,
    },
    {
        path: "dashboard",
        component: DashboardPageComponent,
        canActivate: [AuthenticationGuard()]
    },
    {
        path: "**",
        component: HomePageComponent,
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {
}
