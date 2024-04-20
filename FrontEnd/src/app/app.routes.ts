import {LoginPageComponent} from "./components/pages/login-page/login-page.component";
import {SignupPageComponent} from "./components/pages/signup-page/signup-page.component";
import {HomePageComponent} from "./components/pages/home-page/home-page.component";

import { Routes } from '@angular/router';

export const routes: Routes =
    [
        { path: '', redirectTo: 'login', pathMatch: 'full' },
        { path: 'login', component: LoginPageComponent },
        { path: 'signup', component: SignupPageComponent },
        { path: 'home', component: HomePageComponent }
    ];
