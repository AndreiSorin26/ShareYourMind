import {BaseEntity} from "./base-entity";

export interface Post extends BaseEntity
{
    posterDisplayName: string
    text: string
    loveReactionCount: number
    laughReactionCount: number
    dislikeReactionCount: number
}
