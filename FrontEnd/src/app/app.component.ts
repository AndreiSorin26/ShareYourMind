import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NzMessageService} from "ng-zorro-antd/message";
import {NzModalService} from "ng-zorro-antd/modal";
import {DatePipe} from "@angular/common";

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet],
    providers: [NzMessageService, NzModalService, DatePipe],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})

export class AppComponent
{}
