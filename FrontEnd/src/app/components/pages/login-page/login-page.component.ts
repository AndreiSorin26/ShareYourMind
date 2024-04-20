import {LoginRequest} from "../../../interfaces/login-request";
import {AuthenticationService} from "../../../services/authentication.service";

import { Component } from '@angular/core';
import {NzInputDirective, NzInputGroupComponent} from "ng-zorro-antd/input";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {FormsModule} from "@angular/forms";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {HttpErrorResponse} from "@angular/common/http";
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";
import {NzMessageService} from "ng-zorro-antd/message";
import {LoginRequestResponse} from "../../../interfaces/login-request-response";

@Component({
  selector: 'app-login-page',
  standalone: true,
    imports:
        [
            FormsModule,
            NzInputGroupComponent,
            NzIconDirective,
            NzInputDirective,
            NzButtonComponent
        ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})

export class LoginPageComponent
{
    loginRequest: LoginRequest = { displayName: '', password: '' };

    passwordVisible: boolean = false;
    processingLoginRequest: boolean = false;

    constructor(private authService: AuthenticationService,
                private cookieService: CookieService,
                private router: Router,
                private messageService: NzMessageService)
    {}

    sendLoginRequest(): void
    {
        this.processingLoginRequest = true;
        this.authService.login(this.loginRequest, {
            onSuccess: (response: LoginRequestResponse) => {
                this.cookieService.set('token', response.token)
                this.router.navigate(['/home']).then()
            },
            onError: (error: HttpErrorResponse) => this.messageService.error(error.error),
            onFinally: () => this.processingLoginRequest = false
        })
    }
}
