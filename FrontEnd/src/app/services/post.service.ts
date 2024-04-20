import {CustomHttpResponseHandlers, CustomHttpService} from "./custom-http.service";
import {BACKEND_URL, POST_CONTROLLER} from "../helpers/constants";
import {Post} from "../interfaces/post";

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class PostService
{
    constructor(private http: CustomHttpService)
    {}

    public getFeedPosts(batchIndex: number, handlers: CustomHttpResponseHandlers<Post[]>)
    {
        const url = `${POST_CONTROLLER}/feed/${batchIndex}`
        this.http.get(url, true, undefined, handlers)
    }

    public getPersonalPosts(batchIndex: number, handlers: CustomHttpResponseHandlers<Post[]>)
    {
        const url = `${POST_CONTROLLER}/personal/${batchIndex}`
        this.http.get(url, true, undefined, handlers)
    }

    public updatePost(post: Post, handlers: CustomHttpResponseHandlers<any>)
    {
        this.http.put(POST_CONTROLLER, post, true, undefined, handlers)
    }

    public createPost(text: string, handlers: CustomHttpResponseHandlers<Post>)
    {
        this.http.post(POST_CONTROLLER, {text: text}, true, undefined, handlers)
    }

    public deletePost(id: string, force: boolean, handlers: CustomHttpResponseHandlers<Post>)
    {
        let url = `${POST_CONTROLLER}${force ? '/force' : ''}`
        this.http.delete(url, id, true, undefined, handlers)
    }

    public getPost(id: string, handlers: CustomHttpResponseHandlers<Post>)
    {
        const url = `${POST_CONTROLLER}/${id}`
        this.http.get(url, true, undefined, handlers)
    }
}
