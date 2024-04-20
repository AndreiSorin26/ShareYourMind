import {CustomHttpResponseHandlers, CustomHttpService} from "./custom-http.service";
import {LoginRequest} from "../interfaces/login-request";
import {AUTHENTICATION_CONTROLLER, BACKEND_URL} from "../helpers/constants";

import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {CookieService} from "ngx-cookie-service";
import {LoginRequestResponse} from "../interfaces/login-request-response";

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService
{
    constructor(private http: CustomHttpService,
                private router: Router,
                private cookieService: CookieService)
    {}

    public login(loginRequest: LoginRequest, handlers: CustomHttpResponseHandlers<LoginRequestResponse>)
    {
        this.http.post(AUTHENTICATION_CONTROLLER, loginRequest, false, undefined, handlers);
    }

    public logout()
    {
        this.router.navigate(['/login']).then(() => this.cookieService.delete('token'))
    }
}
