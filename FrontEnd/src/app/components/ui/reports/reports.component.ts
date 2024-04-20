import {Report} from "../../../interfaces/report";
import {ReportingService} from "../../../services/reporting.service";
import {UserService} from "../../../services/user.service";

import {Component, OnInit} from '@angular/core';
import {DatePipe} from "@angular/common";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {
    NzListComponent,
    NzListEmptyComponent,
    NzListItemComponent, NzListItemExtraComponent, NzListItemMetaAvatarComponent,
    NzListItemMetaComponent, NzListItemMetaDescriptionComponent,
    NzListItemMetaTitleComponent
} from "ng-zorro-antd/list";
import {NzMessageService} from "ng-zorro-antd/message";
import {NzInputDirective} from "ng-zorro-antd/input";
import {NzModalComponent, NzModalContentDirective} from "ng-zorro-antd/modal";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Post} from "../../../interfaces/post";
import {PostService} from "../../../services/post.service";
import {NzDividerComponent} from "ng-zorro-antd/divider";
import {NzPopconfirmDirective} from "ng-zorro-antd/popconfirm";

@Component({
  selector: 'app-reports',
  standalone: true,
    imports: [
        DatePipe,
        NzButtonComponent,
        NzIconDirective,
        NzListComponent,
        NzListEmptyComponent,
        NzListItemComponent,
        NzListItemMetaComponent,
        NzListItemMetaTitleComponent,
        NzInputDirective,
        NzModalComponent,
        ReactiveFormsModule,
        NzModalContentDirective,
        FormsModule,
        NzDividerComponent,
        NzListItemExtraComponent,
        NzListItemMetaAvatarComponent,
        NzListItemMetaDescriptionComponent,
        NzPopconfirmDirective
    ],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})

export class ReportsComponent implements OnInit
{
    fetchingReports: boolean = false
    reports: Report[] = []

    postModalVisible: boolean = false
    fetchingPost: boolean = false
    post?: Post = undefined

    constructor(private reportingService: ReportingService,
                private postService: PostService,
                private userService: UserService,
                private messagesService: NzMessageService)
    {}

    ngOnInit()
    {
        this.fetchingReports = true
        this.reportingService.getReports({
            onSuccess: (reports) => this.reports = reports,
            onError: () => this.messagesService.error("Could not fetch reports."),
            onFinally: () => this.fetchingReports = false
        })
    }

    loadPost(postId: string)
    {
        this.fetchingPost = true
        this.postModalVisible = true
        this.postService.getPost(postId, {
            onSuccess: (post) => this.post = post,
            onError: () =>
            {
                this.messagesService.error("Could not fetch post.")
                this.postModalVisible = false
            },
            onFinally: () => this.fetchingPost = false
        })
    }

    deletePost(postId: string)
    {
        this.postService.deletePost(postId, true, {
            onSuccess: () =>
            {
                this.messagesService.success("Post deleted.")
                this.reports = this.reports.filter(report => report.postId !== postId)
            },
            onError: () => this.messagesService.error("Could not delete post.")
        })
    }

    closeReport(reportId: string)
    {
        this.userService.closeReport(reportId, {
            onSuccess: () =>
            {
                this.messagesService.success("Report closed.")
                this.reports = this.reports.filter(report => report.id !== reportId)
            },
            onError: () => this.messagesService.error("Could not close report.")
        })
    }
}
