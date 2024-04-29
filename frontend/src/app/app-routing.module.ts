import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { DiscordCallbackPageComponent } from "./pages/auth/discord-callback-page/discord-callback-page.component";

const routes: Routes = [
    {
        path: "",
        component: HomePageComponent,
    },
    {
        path: "auth/discord",
        component: DiscordCallbackPageComponent,
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
