import {User} from "../../../interfaces/user";
import {FriendRequest, FriendRequestStatus} from "../../../interfaces/friend-request";
import {UserService} from "../../../services/user.service";
import {AuthenticationService} from "../../../services/authentication.service";
import {FeedComponent} from "../feed/feed.component";
import {PostService} from "../../../services/post.service";

import {Component, Input, OnInit} from '@angular/core';
import {NzInputDirective, NzInputGroupComponent} from "ng-zorro-antd/input";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {NzTooltipDirective} from "ng-zorro-antd/tooltip";
import {NzDividerComponent} from "ng-zorro-antd/divider";
import {FormsModule} from "@angular/forms";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {NzListComponent, NzListEmptyComponent, NzListItemComponent} from "ng-zorro-antd/list";
import {NzMessageService} from "ng-zorro-antd/message";
import {NzInputNumberComponent} from "ng-zorro-antd/input-number";
import {NzEmptyComponent} from "ng-zorro-antd/empty";
import {NzTabComponent, NzTabSetComponent} from "ng-zorro-antd/tabs";

@Component({
  selector: 'app-profile',
  standalone: true,
    imports: [
        NzInputGroupComponent,
        NzInputDirective,
        NzIconDirective,
        NzTooltipDirective,
        NzDividerComponent,
        FormsModule,
        NzButtonComponent,
        NzListComponent,
        NzListItemComponent,
        NzListEmptyComponent,
        NzInputNumberComponent,
        NzEmptyComponent,
        NzTabSetComponent,
        NzTabComponent,
        FeedComponent
    ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})

export class ProfileComponent implements OnInit
{
    protected readonly FriendRequestStatus = FriendRequestStatus;

    @Input() user?: User

    receiverUsername?: string = undefined
    receiverTag?: number = undefined
    sendingFriendRequest: boolean = false

    friendRequests: FriendRequest[] = []

    constructor(private userService: UserService,
                private authService: AuthenticationService,
                protected postService: PostService,
                private messageService: NzMessageService)
    {}

    ngOnInit()
    {
        this.userService.getFriendRequests({
            onSuccess: friendRequests => this.friendRequests = friendRequests,
            onError: () => this.messageService.error('Failed to get friend requests.')
        })
    }

    sendFriendRequest()
    {
        if(!this.receiverUsername || !this.receiverTag)
        {
            this.messageService.error('Please enter a username and a tag.')
            return;
        }

        this.sendingFriendRequest = true
        this.userService.sendFriendRequest(this.receiverUsername, this.receiverTag, {
            onSuccess: () => this.messageService.success('Friend request sent.'),
            onError: () => this.messageService.error('Failed to send friend request.'),
            onFinally: () => {
                this.sendingFriendRequest = false
                this.receiverUsername = undefined
                this.receiverTag = undefined
            }
        })
    }

    updateFriendRequests(friendRequest: FriendRequest, status: FriendRequestStatus)
    {
        friendRequest.status = status
        this.userService.updateFriendRequest(friendRequest, {
            onSuccess: () => this.messageService.success('Friend request updated.'),
            onError: () => this.messageService.error('Failed to update friend request.')
        })
    }

    filterPendingFriendRequests()
    {
        return this.friendRequests.filter(friendRequest => friendRequest.status === FriendRequestStatus.Pending && friendRequest.receiverDisplayName === this.user?.displayName)
    }

    filterAcceptedFriendRequests()
    {
        return this.friendRequests.filter(friendRequest => friendRequest.status === FriendRequestStatus.Accepted)
    }

    filterRefusedFriendRequests()
    {
        return this.friendRequests.filter(friendRequest => friendRequest.status === FriendRequestStatus.Refused && friendRequest.receiverDisplayName === this.user?.displayName)
    }

    logout()
    {
        this.authService.logout();
    }
}
