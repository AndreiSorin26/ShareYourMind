import {CustomHttpResponseHandlers, CustomHttpService} from "./custom-http.service";
import {REPORT_CONTROLLER} from "../helpers/constants";
import {Post} from "../interfaces/post";
import {Report} from "../interfaces/report";

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ReportingService {

    constructor(private http: CustomHttpService)
    {}

    public getReports(handlers: CustomHttpResponseHandlers<Report[]>)
    {
        this.http.get(REPORT_CONTROLLER, true, undefined, handlers);
    }

    public updateReport(report: Report, handlers: CustomHttpResponseHandlers<any>)
    {
        this.http.put(REPORT_CONTROLLER, report, true, undefined, handlers);
    }

    public sendReport(reportedPost: Post, text: string, handlers: CustomHttpResponseHandlers<any>)
    {
        this.http.post(REPORT_CONTROLLER, {reportedPost, text}, true, undefined, handlers);
    }
}
