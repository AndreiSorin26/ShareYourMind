import {CustomHttpResponseHandlers, CustomHttpService} from "./custom-http.service";

import { Injectable } from '@angular/core';
import {Comment} from "../interfaces/comment";
import {COMMENT_CONTROLLER} from "../helpers/constants";

@Injectable({
  providedIn: 'root'
})
export class CommentService {

    constructor(private http: CustomHttpService)
    {}

    public getComments(postId: string, handlers: CustomHttpResponseHandlers<Comment[]>)
    {
        const url = `${COMMENT_CONTROLLER}/${postId}`
        this.http.get(url, true, undefined, handlers);
    }

    public postComment(postId: string, text: string, handlers: CustomHttpResponseHandlers<Comment>)
    {
        this.http.post(COMMENT_CONTROLLER, {postId, text}, true, undefined, handlers);
    }

    public updateComment(comment: Comment, handlers: CustomHttpResponseHandlers<any>)
    {
        this.http.put(COMMENT_CONTROLLER, comment, true, undefined, handlers);
    }

    public deleteComment(commentId: string, handlers: CustomHttpResponseHandlers<any>)
    {
        this.http.delete(COMMENT_CONTROLLER, commentId, true, undefined, handlers);
    }
}
