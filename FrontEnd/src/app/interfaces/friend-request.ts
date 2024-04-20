import {BaseEntity} from "./base-entity";

export enum FriendRequestStatus
{
    Accepted = 0,
    Pending = 1,
    Refused = 2
}

export interface FriendRequest extends BaseEntity
{
    senderDisplayName: string
    receiverDisplayName: string
    status: FriendRequestStatus
}
