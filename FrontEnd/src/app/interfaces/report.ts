import {BaseEntity} from "./base-entity";

export interface Report extends BaseEntity
{
    postId: string
    reportingUserDisplayName: string
    text: string
    closed: boolean
}
