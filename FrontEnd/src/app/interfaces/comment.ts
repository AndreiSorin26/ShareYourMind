import {BaseEntity} from "./base-entity";

export interface Comment extends BaseEntity
{
    text: string
    posterDisplayName: string
}
