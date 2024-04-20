import {Component, Input, OnInit} from '@angular/core';
import { User } from '../../../interfaces/user';
import {Post} from "../../../interfaces/post";
import {PostService} from "../../../services/post.service";
import {Comment} from "../../../interfaces/comment";
import {CommentService} from "../../../services/comment.service";
import {ReportingService} from "../../../services/reporting.service";

import {
    NzListComponent, NzListEmptyComponent, NzListItemActionComponent,
    NzListItemComponent, NzListItemExtraComponent,
    NzListItemMetaAvatarComponent, NzListItemMetaComponent, NzListItemMetaDescriptionComponent,
    NzListItemMetaTitleComponent
} from "ng-zorro-antd/list";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {NzMessageService} from "ng-zorro-antd/message";
import {NzDividerComponent} from "ng-zorro-antd/divider";
import {NzInputDirective, NzTextareaCountComponent} from "ng-zorro-antd/input";
import {FormsModule} from "@angular/forms";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {DatePipe} from "@angular/common";
import {NzSpinComponent} from "ng-zorro-antd/spin";
import {NzModalComponent, NzModalContentDirective} from "ng-zorro-antd/modal";
import {UserService} from "../../../services/user.service";
import {NzPopoverDirective} from "ng-zorro-antd/popover";
import {HttpErrorResponse} from "@angular/common/http";
import {LoadingSpinnerComponent} from "../loading-spinner/loading-spinner.component";

@Component({
  selector: 'app-feed',
  standalone: true,
    imports: [
        NzListComponent,
        NzListItemComponent,
        NzListItemMetaAvatarComponent,
        NzListItemMetaTitleComponent,
        NzListItemMetaDescriptionComponent,
        NzListItemActionComponent,
        NzListItemExtraComponent,
        NzListItemMetaComponent,
        NzIconDirective,
        NzDividerComponent,
        NzTextareaCountComponent,
        NzInputDirective,
        FormsModule,
        NzButtonComponent,
        DatePipe,
        NzListEmptyComponent,
        NzSpinComponent,
        NzModalComponent,
        NzModalContentDirective,
        NzPopoverDirective,
        LoadingSpinnerComponent
    ],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})

export class FeedComponent implements OnInit
{
    @Input() user: User | undefined = undefined
    @Input() ownPosts: boolean = false
    @Input() editablePosts: boolean = false

    posts: Post[] = []
    loadingPosts: boolean = false
    batchIndex: number = 0

    newPostText: string = ''
    uploadingPost: boolean = false

    commentModalVisible: boolean = false
    loadingComments: boolean = false
    comments: Comment[] = []
    selectedPost?: Post = undefined

    newCommentText: string = ''
    uploadingComment: boolean = false

    selectedPostForEdit?: Post = undefined
    editPostModalVisible: boolean = false
    savingEditedPost: boolean = false

    reportModalVisible: boolean = false
    selectedPostForReport?: Post = undefined
    newReportText: string = ''
    uploadingReport: boolean = false

    constructor(private postService: PostService,
                private userService: UserService,
                private commentService: CommentService,
                private reportingService: ReportingService,
                private messageService: NzMessageService)
    {}

    ngOnInit()
    {
        this.loadingPosts = true
        if(this.ownPosts)
            this.postService.getPersonalPosts(0, {
                onSuccess: (posts: Post[]) => this.posts = posts,
                onError: (error) => this.messageService.error(error.error),
                onFinally: () => this.loadingPosts = false
            });
        else
            this.postService.getFeedPosts(0, {
                onSuccess: (posts: Post[]) => this.posts = posts,
                onError: (error) => this.messageService.error(error.error),
                onFinally: () => this.loadingPosts = false
            });
    }

    uploadPost()
    {
        this.uploadingPost = true
        this.postService.createPost(this.newPostText, {
            onSuccess: () => {
                this.newPostText = ''
                this.messageService.success('Post uploaded successfully');
            },
            onError: (error) => this.messageService.error(error.error),
            onFinally: () => this.uploadingPost = false
        })
    }

    loadMorePosts()
    {
        this.loadingPosts = true
        this.batchIndex++

        const handlers =
        {
            onSuccess: (posts: Post[]) =>
            {
                if (posts.length === 0)
                {
                    this.messageService.info('No more posts to show');
                    this.batchIndex--;
                }
                else
                {
                    this.posts = this.posts.concat(posts);
                }
            },
            onError: (error: HttpErrorResponse) =>
            {
                this.messageService.error(error.error)
                this.batchIndex--;
            },
            onFinally: () => this.loadingPosts = false
        }

        if(this.ownPosts)
            this.postService.getPersonalPosts(this.batchIndex, handlers);
        else
            this.postService.getFeedPosts(this.batchIndex, handlers);
    }

    openComments(post: Post)
    {
        this.commentModalVisible = true
        this.loadingComments = true
        this.commentService.getComments(post.id, {
            onSuccess: (comments: Comment[]) =>
            {
                this.comments = comments
                this.selectedPost = post
            },
            onError: () => this.messageService.error("Could not get comments"),
            onFinally: () => this.loadingComments = false
        })
    }

    uploadComment()
    {
        this.uploadingComment = true
        this.commentService.postComment(this.selectedPost!.id, this.newCommentText, {
            onSuccess: (newComment: Comment) => this.comments.unshift(newComment),
            onError: (error) => this.messageService.error(error.error),
            onFinally: () =>
            {
                this.newCommentText = ''
                this.uploadingComment = false
            }
        })
    }

    openReport(post: Post)
    {
        this.selectedPostForReport = post
        this.reportModalVisible = true
    }

    cancelReport()
    {
        this.reportModalVisible = false
        this.newReportText = ''
        this.selectedPostForReport = undefined
    }

    sendReport()
    {
        this.uploadingReport = true
        this.reportingService.sendReport(this.selectedPostForReport!, this.newReportText, {
            onSuccess: () =>
            {
                this.messageService.success('Report sent successfully');
                this.cancelReport();
            },
            onError: () => this.messageService.error("Could not send report"),
            onFinally: () => this.uploadingReport = false
        })
    }

    handlePostReaction(post: Post, reactionType: 'Love' | 'Laugh' | 'Dislike')
    {
        this.userService.addPostReaction(post.id, reactionType, {
            onSuccess: () =>
            {
                if(this.isOwnPostReaction(post, reactionType))
                {
                    switch (reactionType)
                    {
                        case 'Love':
                            post.loveReactionCount--;
                            this.user!.loveReactionPosts = this.user!.loveReactionPosts.filter(p => p.id !== post.id)
                            break;
                        case 'Laugh':
                            post.laughReactionCount--;
                            this.user!.laughReactionPosts = this.user!.laughReactionPosts.filter(p => p.id !== post.id)
                            break;
                        case 'Dislike':
                            post.dislikeReactionCount--;
                            this.user!.dislikeReactionPosts = this.user!.dislikeReactionPosts.filter(p => p.id !== post.id)
                            break;
                    }
                }
                else
                {
                    switch (reactionType)
                    {
                        case 'Love':
                            post.loveReactionCount++;
                            this.user!.loveReactionPosts.push(post)

                            if(this.user!.laughReactionPosts.some(p => p.id === post.id))
                            {
                                post.laughReactionCount--;
                                this.user!.laughReactionPosts = this.user!.laughReactionPosts.filter(p => p.id !== post.id)
                            }
                            if(this.user!.dislikeReactionPosts.some(p => p.id === post.id))
                            {
                                post.dislikeReactionCount--;
                                this.user!.dislikeReactionPosts = this.user!.dislikeReactionPosts.filter(p => p.id !== post.id)
                            }
                            break;
                        case 'Laugh':
                            post.laughReactionCount++;
                            this.user!.laughReactionPosts.push(post)

                            if(this.user!.loveReactionPosts.some(p => p.id === post.id))
                            {
                                post.loveReactionCount--;
                                this.user!.loveReactionPosts = this.user!.loveReactionPosts.filter(p => p.id !== post.id)
                            }
                            if(this.user!.dislikeReactionPosts.some(p => p.id === post.id))
                            {
                                post.dislikeReactionCount--;
                                this.user!.dislikeReactionPosts = this.user!.dislikeReactionPosts.filter(p => p.id !== post.id)
                            }
                            break;
                        case 'Dislike':
                            post.dislikeReactionCount++;
                            this.user!.dislikeReactionPosts.push(post)

                            if(this.user!.laughReactionPosts.some(p => p.id === post.id))
                            {
                                post.laughReactionCount--;
                                this.user!.laughReactionPosts = this.user!.laughReactionPosts.filter(p => p.id !== post.id)
                            }
                            if(this.user!.loveReactionPosts.some(p => p.id === post.id))
                            {
                                post.loveReactionCount--;
                                this.user!.loveReactionPosts = this.user!.loveReactionPosts.filter(p => p.id !== post.id)
                            }
                            break;
                    }
                }
            },
            onError: () => this.messageService.error("Failed to react to post")
        })
    }

    isOwnPostReaction(post: Post, reactionType: 'Love' | 'Laugh' | 'Dislike')
    {
        switch (reactionType)
        {
            case 'Love': return this.user?.loveReactionPosts.some(p => p.id === post.id)
            case 'Laugh': return this.user?.laughReactionPosts.some(p => p.id === post.id)
            case 'Dislike': return this.user?.dislikeReactionPosts.some(p => p.id === post.id)
        }
    }

    openPostForEdit(post: Post)
    {
        this.editPostModalVisible = true
        this.selectedPostForEdit = post
    }

    saveEditedPost()
    {
        this.savingEditedPost = true
        this.postService.updatePost(this.selectedPostForEdit!, {
            onSuccess: () => this.messageService.success('Post updated successfully'),
            onError: () => this.messageService.error('Failed to update post'),
            onFinally: () =>
            {
                this.savingEditedPost = false
                this.editPostModalVisible = false
            }
        })
    }

    deletePost(post: Post)
    {
        this.postService.deletePost(post.id, false, {
            onSuccess: () =>
            {
                this.posts = this.posts.filter(p => p.id !== post.id)
                this.messageService.success('Post deleted successfully')
            },
            onError: () => this.messageService.error('Failed to delete post')
        })
    }
}
