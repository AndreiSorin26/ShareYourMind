import {CustomHttpResponseHandlers, CustomHttpService} from "./custom-http.service";
import {NewFeedback} from "../interfaces/new-feedback";
import {FEEDBACK_CONTROLLER} from "../helpers/constants";

import { Injectable } from '@angular/core';
import {FeedbackMetric} from "../interfaces/feedback-metric";
import {Feedback} from "../interfaces/feedback";

@Injectable({
  providedIn: 'root'
})

export class FeedbackService
{
    constructor(private http: CustomHttpService)
    {}

    public submitFeedback(feedback: NewFeedback, handlers: CustomHttpResponseHandlers<any>)
    {
        return this.http.post(FEEDBACK_CONTROLLER, feedback, true, undefined, handlers);
    }

    public getGoodFeedbacks(startDate: Date, endDate: Date, handlers: CustomHttpResponseHandlers<Feedback[]>)
    {
        const url = `${FEEDBACK_CONTROLLER}/good/${startDate.toISOString()}/${endDate.toISOString()}`
        return this.http.get(url, true, undefined, handlers);
    }

    public getBadFeedbacks(startDate: Date, endDate: Date, handlers: CustomHttpResponseHandlers<Feedback[]>)
    {
        const url = `${FEEDBACK_CONTROLLER}/bad/${startDate.toISOString()}/${endDate.toISOString()}`
        return this.http.get(url, true, undefined, handlers);
    }

    public getFeedbacks(startDate: Date, endDate: Date, maxCount: number, handlers: CustomHttpResponseHandlers<Feedback[]>)
    {
        const url = `${FEEDBACK_CONTROLLER}/${startDate.toISOString()}/${endDate.toISOString()}/${maxCount}`
        return this.http.get(url, true, undefined, handlers);
    }

    public getFeedbackMetrics(startDate: Date, endDate: Date, handlers: CustomHttpResponseHandlers<FeedbackMetric[]>)
    {
        const url = `${FEEDBACK_CONTROLLER}/metrics/${startDate.toISOString()}/${endDate.toISOString()}`
        return this.http.get(url, true, undefined, handlers);
    }
}
