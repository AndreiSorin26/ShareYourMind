import {FeedComponent} from "../../ui/feed/feed.component";
import {AdminRequestsComponent} from "../../ui/admin-requests/admin-requests.component";
import {ProfileComponent} from "../../ui/profile/profile.component";
import {UserService} from "../../../services/user.service";
import {User} from "../../../interfaces/user";
import {PostService} from "../../../services/post.service";

import {Component, OnInit} from "@angular/core";
import {NzTabComponent, NzTabDirective, NzTabSetComponent} from "ng-zorro-antd/tabs";
import {NzModalComponent, NzModalContentDirective} from "ng-zorro-antd/modal";
import {NzOptionComponent, NzSelectComponent} from "ng-zorro-antd/select";
import {FormsModule} from "@angular/forms";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {ReportsComponent} from "../../ui/reports/reports.component";
import {FeedbackComponent} from "../../ui/feedback/feedback.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
    imports: [
        NzTabSetComponent,
        NzTabComponent,
        NzTabDirective,
        FeedComponent,
        NzModalComponent,
        NzModalContentDirective,
        NzSelectComponent,
        NzOptionComponent,
        FormsModule,
        AdminRequestsComponent,
        ProfileComponent,
        NzIconDirective,
        NzButtonComponent,
        ReportsComponent,
        FeedbackComponent
    ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})

export class HomePageComponent implements OnInit
{
    user: User | undefined = undefined;

    constructor(private userService: UserService,
                protected postService: PostService)
    {}

    ngOnInit(): void
    {
        this.userService.getUser({
            onSuccess: (user) => this.user = user,
            onError: (error) => console.error('Error getting user', error)
        });
    }
}
