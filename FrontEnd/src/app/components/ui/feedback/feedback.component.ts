import {User} from "../../../interfaces/user";
import {NewFeedback} from "../../../interfaces/new-feedback";
import {FeedbackService} from "../../../services/feedback.service";
import {Feedback} from "../../../interfaces/feedback";
import {FeedbackMetric} from "../../../interfaces/feedback-metric";
import {LoadingSpinnerComponent} from "../loading-spinner/loading-spinner.component";
import {CustomHttpResponseHandlers} from "../../../services/custom-http.service";

import {Component, Input, OnInit} from '@angular/core';
import {NzInputDirective} from "ng-zorro-antd/input";
import {FormsModule} from "@angular/forms";
import {NzRateComponent} from "ng-zorro-antd/rate";
import {DatePipe, DecimalPipe, NgOptimizedImage, NgSwitch, NgSwitchCase} from "@angular/common";
import {NzIconDirective} from "ng-zorro-antd/icon";
import {NzButtonComponent} from "ng-zorro-antd/button";
import {NzDividerComponent} from "ng-zorro-antd/divider";
import {NzMessageService} from "ng-zorro-antd/message";
import {NzOptionComponent, NzSelectComponent} from "ng-zorro-antd/select";
import {
    NzListComponent,
    NzListEmptyComponent,
    NzListItemComponent,
    NzListItemExtraComponent,
    NzListItemMetaAvatarComponent,
    NzListItemMetaComponent,
    NzListItemMetaDescriptionComponent,
    NzListItemMetaTitleComponent
} from "ng-zorro-antd/list";
import {NzPopoverDirective} from "ng-zorro-antd/popover";
import {NzDatePickerComponent} from "ng-zorro-antd/date-picker";
import {NzProgressComponent} from "ng-zorro-antd/progress";
import { ChartModule } from 'primeng/chart';
import {NzStatisticComponent} from "ng-zorro-antd/statistic";

@Component({
  selector: 'app-feedback',
  standalone: true,
    imports: [
        NzInputDirective,
        FormsModule,
        NzRateComponent,
        NgSwitch,
        NzIconDirective,
        NgSwitchCase,
        NzButtonComponent,
        NzDividerComponent,
        NzSelectComponent,
        NzOptionComponent,
        DatePipe,
        LoadingSpinnerComponent,
        NzListComponent,
        NzListEmptyComponent,
        NzListItemComponent,
        NzListItemExtraComponent,
        NzListItemMetaAvatarComponent,
        NzListItemMetaComponent,
        NzListItemMetaDescriptionComponent,
        NzListItemMetaTitleComponent,
        NzPopoverDirective,
        NgOptimizedImage,
        NzDatePickerComponent,
        NzProgressComponent,
        ChartModule,
        NzStatisticComponent,
        DecimalPipe
    ],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.css'
})

export class FeedbackComponent implements OnInit
{
    @Input() user?: User = undefined

    postingFeedback: boolean = false
    newFeedback: NewFeedback = { text: "", uiRating: 0, dataFlowRating: 0, uxRating: 0, communityRating: 0 }

    feedbackTypes: string[] = ["All", "Good", "Bad", "Statistics"]
    selectedFeedbackType: string = "All"
    startDate: Date = new Date(0)
    endDate: Date = new Date(Date.now())

    gettingFeedbacks: boolean = false
    feedbacks: Feedback[] = []
    feedbackMetrics: FeedbackMetric[] = []

    pieChartOptions =
    {
        plugins: {
            legend: {
                labels: {
                    usePointStyle: true,
                    color: "#000000"
                }
            }
        }
    };

    constructor(private feedbackService: FeedbackService,
                private messageService: NzMessageService)
    {}

    ngOnInit()
    {
        if(this.user?.role === "Admin")
        {
            this.gettingFeedbacks = true
            this.feedbackService.getFeedbacks(new Date(0), new Date(Date.now()), 25, {
                onSuccess: (data: Feedback[]) => this.feedbacks = data,
                onError: () => this.messageService.error("Failed to get feedbacks"),
                onFinally: () => this.gettingFeedbacks = false
            })
        }
    }

    postFeedback()
    {
        this.postingFeedback = true
        this.feedbackService.submitFeedback(this.newFeedback, {
            onSuccess: () =>
            {
                this.messageService.success("Feedback submitted successfully");
                this.newFeedback = { text: "", uiRating: 0, dataFlowRating: 0, uxRating: 0, communityRating: 0 }
            },
            onError: (error) => this.messageService.error(`Failed to submit feedback: ${error.error}`),
            onFinally: () => this.postingFeedback = false
        })
    }

    reloadFeedbacks()
    {
        this.gettingFeedbacks = true

        const handlers: CustomHttpResponseHandlers<Feedback[]> =
            {
                onSuccess: (data: Feedback[]) => this.feedbacks = data,
                onError: () => this.messageService.error("Failed to get feedbacks"),
                onFinally: () => this.gettingFeedbacks = false
            }

        switch (this.selectedFeedbackType)
        {
            case "All":
                this.feedbackMetrics = []
                this.feedbackService.getFeedbacks(this.startDate, this.endDate, 25, handlers)
                break
            case "Good":
                this.feedbackMetrics = []
                this.feedbackService.getGoodFeedbacks(this.startDate, this.endDate, handlers)
                break
            case "Bad":
                this.feedbackMetrics = []
                this.feedbackService.getBadFeedbacks(this.startDate, this.endDate, handlers)
                break
            case "Statistics":
                this.feedbacks = []
                this.feedbackService.getFeedbackMetrics(this.startDate, this.endDate, {
                    onSuccess: (data: FeedbackMetric[]) => this.feedbackMetrics = data,
                    onError: () => this.messageService.error("Failed to get feedback metrics"),
                    onFinally: () => this.gettingFeedbacks = false
                })
                break
        }
    }

    getTypeFeedbackMetrics(type: string): FeedbackMetric[]
    {
        return this.feedbackMetrics.filter(metric => metric.type === type)
    }

    getRandomColor(): string
    {
        return `#${Math.floor(Math.random()*16777215).toString(16)}`
    }

    getFeedbackMetricValues(metrics: FeedbackMetric[]): number[]
    {
        return metrics.map(metric => metric.value)
    }

    getFeedbackMetricNames(metrics: FeedbackMetric[]): string[]
    {
        return metrics.map(metric => metric.name)
    }

    makePieChartData(content: string)
    {
        const feedbackMetrics = this.feedbackMetrics.filter(metric => metric.type == 'Percentage')
        const metrics = feedbackMetrics.filter(metric => metric.name.includes(content) && metric.value > 0)
        return {
            labels: this.getFeedbackMetricNames(metrics),
            datasets: [
                {
                    data: this.getFeedbackMetricValues(metrics),
                    backgroundColor: this.getFeedbackMetricValues(metrics).map(() => this.getRandomColor()),
                    hoverBackgroundColor: this.getFeedbackMetricValues(metrics).map(() => this.getRandomColor())
                }
            ]
        }
    }
}
