import {USERNAME_RESTRICTIONS} from "../../../helpers/constants";
import {SignupRequest} from "../../../interfaces/signup-request";
import {UserService} from "../../../services/user.service";
import {User} from "../../../interfaces/user";

import {Component} from '@angular/core';
import {NzButtonComponent} from "ng-zorro-antd/button";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {NzInputDirective, NzInputGroupComponent} from "ng-zorro-antd/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NzOptionComponent, NzSelectComponent} from "ng-zorro-antd/select";
import {NzMessageService} from "ng-zorro-antd/message";
import {HttpErrorResponse} from "@angular/common/http";
import {NzNotificationService} from "ng-zorro-antd/notification";

@Component({
  selector: 'app-signup-page',
  standalone: true,
    imports:
        [
            NzButtonComponent,
            NzIconDirective,
            NzInputDirective,
            NzInputGroupComponent,
            ReactiveFormsModule,
            FormsModule,
            NzSelectComponent,
            NzOptionComponent
        ],
  templateUrl: './signup-page.component.html',
  styleUrl: './signup-page.component.css'
})

export class SignupPageComponent
{
    userRoles = ['User', 'Admin']
    signUpRequest: SignupRequest = { username: '', password: '', role: 'User' }

    passwordVisible: boolean = false
    processingSignUpRequest: boolean = false

    constructor(private userService: UserService,
                private messageService: NzMessageService,
                private notificationService: NzNotificationService)
    {}

    sendSignUpRequest(): void
    {
        const invalid = this.signUpRequest.username.matchAll(USERNAME_RESTRICTIONS)
        if(!invalid.next().done)
        {
            console.log(invalid)
            this.messageService.error('Username must only contains letters and numbers')
            return;
        }

        this.processingSignUpRequest = true
        this.userService.addUser(this.signUpRequest, {
            onSuccess: (newUser: User) =>
            {
                this.notificationService.blank(
                    'User successfully created',
                    `The display name you will use for login is ${newUser.displayName}`,
                    {
                        nzPlacement: 'bottomRight' ,
                        nzDuration: 10000
                    }
                );
                this.signUpRequest = { username: '', password: '', role: 'User' }
            },
            onError: (error: HttpErrorResponse) => this.messageService.error(error.error.message),
            onFinally: () => this.processingSignUpRequest = false
        })
    }
}
