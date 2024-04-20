import {BaseEntity} from "./base-entity";

export interface Feedback extends BaseEntity
{
    userDisplayName: string
    text: string
    uiRating: number
    dataFlowRating: number
    uxRating: number
    communityRating: number
}
