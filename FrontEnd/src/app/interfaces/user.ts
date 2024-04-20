import {BaseEntity} from "./base-entity";
import {Post} from "./post";
import {Feedback} from "./feedback";

export interface User extends BaseEntity
{
    username: string
    tag: string
    displayName: string
    role: 'Admin' | 'User' | 'Guest'
    loveReactionPosts: Post[]
    laughReactionPosts: Post[]
    dislikeReactionPosts: Post[]
}
