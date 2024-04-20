import {UserService} from "../../../services/user.service";

import {Component, OnInit} from '@angular/core';
import {HttpErrorResponse} from "@angular/common/http";
import {NzMessageService} from "ng-zorro-antd/message";

import
{
    NzListComponent,
    NzListEmptyComponent, NzListItemActionComponent,
    NzListItemComponent, NzListItemMetaComponent,
    NzListItemMetaTitleComponent
} from "ng-zorro-antd/list";
import {DatePipe} from "@angular/common";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {User} from "../../../interfaces/user";

@Component({
    selector: 'app-admin-requests',
    standalone: true,
    imports:
        [
            NzListComponent,
            NzListItemComponent,
            NzListItemMetaTitleComponent,
            NzListEmptyComponent,
            NzListItemMetaComponent,
            NzListItemActionComponent,
            DatePipe,
            NzButtonComponent,
            NzIconDirective
        ],
    templateUrl: './admin-requests.component.html',
    styleUrl: './admin-requests.component.css'
})

export class AdminRequestsComponent implements OnInit
{
    newAdmins: User[] = []
    loadingRequests = false;

    constructor(private userService: UserService,
                private messageService: NzMessageService)
    {}

    ngOnInit()
    {
        this.loadingRequests = true
        this.userService.getAdminRequests({
            onSuccess: (adminRequests: User[]) => this.newAdmins = adminRequests,
            onError: (error: HttpErrorResponse) => this.messageService.error(error.error),
            onFinally: () => this.loadingRequests = false
        });
    }

    approveAdminRequest(newAdmin: User)
    {
        this.userService.approveAdminRequest(newAdmin, {
            onSuccess: () =>
            {
                this.newAdmins = this.newAdmins.filter(request => request !== newAdmin)
                this.messageService.success('Admin request approved')
            },
            onError: (error: HttpErrorResponse) => this.messageService.error(error.error)
        });
    }
}
