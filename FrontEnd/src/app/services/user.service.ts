import {CustomHttpResponseHandlers, CustomHttpService} from "./custom-http.service";
import {SignupRequest} from "../interfaces/signup-request";

import { Injectable } from '@angular/core';
import {User} from "../interfaces/user";
import {USER_CONTROLLER} from "../helpers/constants";
import {FriendRequest} from "../interfaces/friend-request";

@Injectable({
  providedIn: 'root'
})

export class UserService
{
    constructor(private http: CustomHttpService)
    {}

    public getUser(handlers: CustomHttpResponseHandlers<User>)
    {
        this.http.get(USER_CONTROLLER, true, undefined, handlers);
    }

    public addUser(signupRequest: SignupRequest, handlers: CustomHttpResponseHandlers<User>)
    {
        this.http.post(USER_CONTROLLER, signupRequest, false, undefined, handlers);
    }

    public getAdminRequests(handlers: CustomHttpResponseHandlers<User[]>)
    {
        const url = `${USER_CONTROLLER}/admin/unapproved`
        this.http.get(url, true, undefined, handlers);
    }

    public approveAdminRequest(admin: User, handlers: CustomHttpResponseHandlers<any>)
    {
        const url = `${USER_CONTROLLER}/admin/approve`
        this.http.put(url, admin, true, undefined, handlers);
    }

    public deleteUser(id: string, handlers: CustomHttpResponseHandlers<any>)
    {
        this.http.delete(USER_CONTROLLER, id, true, undefined, handlers);
    }

    public sendFriendRequest(receiverUsername: string, receiverTag: number, handlers: CustomHttpResponseHandlers<any>)
    {
        const url = `${USER_CONTROLLER}/friend`
        this.http.post(url, {receiverDisplayName: `${receiverUsername}#${receiverTag}`}, true, undefined, handlers);
    }

    public getFriendRequests(handlers: CustomHttpResponseHandlers<FriendRequest[]>)
    {
        const url = `${USER_CONTROLLER}/friend`
        this.http.get(url, true, undefined, handlers);
    }

    public updateFriendRequest(friendRequest: FriendRequest, handlers: CustomHttpResponseHandlers<any>)
    {
        const url = `${USER_CONTROLLER}/friend`
        this.http.put(url, friendRequest, true, undefined, handlers);
    }

    public addPostReaction(postId: string, reactionType: 'Love' | 'Laugh' | 'Dislike', handlers: CustomHttpResponseHandlers<any>)
    {
        const url = `${USER_CONTROLLER}/react`;
        this.http.post(url, {postId, reactionType}, true, undefined, handlers);
    }

    public closeReport(reportId: string, handlers: CustomHttpResponseHandlers<any>)
    {
        const url = `${USER_CONTROLLER}/admin/close-report/${reportId}`;
        this.http.put(url, undefined, true, undefined, handlers);
    }
}
