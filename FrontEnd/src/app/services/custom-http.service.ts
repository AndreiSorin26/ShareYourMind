import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";

export interface CustomHttpResponseHandlers<ResponseType>
{
    onSuccess?: (response: ResponseType) => void;
    onError?: (error: HttpErrorResponse) => void;
    onFinally?: () => void;
}

@Injectable({
  providedIn: 'root'
})
export class CustomHttpService
{
    constructor(private http: HttpClient,
                private cookieService: CookieService,
                private router: Router)
    {}

    private getHeaders(useToken: boolean, headers?: HttpHeaders): HttpHeaders | undefined
    {
        if(useToken)
        {
            headers = headers ?? new HttpHeaders()
            headers = headers.set('Authorization', `Bearer ${this.cookieService.get('token')}`)
        }

        return headers;
    }

    private getObserverObject<ResponseType>(handlers?: CustomHttpResponseHandlers<ResponseType>): any
    {
        const observerObject: any = {};

        observerObject.next = (response: ResponseType) =>
        {
            handlers?.onSuccess?.(response)
            handlers?.onFinally?.()
        }

        observerObject.error = (error: HttpErrorResponse) =>
        {
            if(error.status === 401 || error.status === 403)
                this.router.navigate(['/login']).then(() =>
                {
                    this.cookieService.delete('token');
                    handlers?.onError?.(error)
                    handlers?.onFinally?.()
                });

            handlers?.onError?.(error)
            handlers?.onFinally?.()
        }

        return observerObject
    }

    public get<ResponseType>(url: string, useToken: boolean = false, headers?: HttpHeaders, handlers?: CustomHttpResponseHandlers<ResponseType>)
    {
        const fullHeaders = this.getHeaders(useToken, headers)
        const observerObject = this.getObserverObject(handlers)

        this.http.get<ResponseType>(url, {headers: fullHeaders}).subscribe(observerObject)
    }

    public post<ResponseType, BodyType>(url: string, body: BodyType, useToken: boolean = false, headers?: HttpHeaders, handlers?: CustomHttpResponseHandlers<ResponseType>)
    {
        const fullHeaders = this.getHeaders(useToken, headers)
        const observerObject = this.getObserverObject(handlers)

        this.http.post<ResponseType>(url, body, {headers: fullHeaders}).subscribe(observerObject)
    }

    public put<ResponseType, BodyType>(url: string, body: BodyType, useToken: boolean = false, headers?: HttpHeaders, handlers?: CustomHttpResponseHandlers<ResponseType>)
    {
        const fullHeaders = this.getHeaders(useToken, headers)
        const observerObject = this.getObserverObject(handlers)

        this.http.put<ResponseType>(url, body, {headers: fullHeaders}).subscribe(observerObject)
    }

    public delete<ResponseType>(url: string, id: string, useToken: boolean = false, headers?: HttpHeaders, handlers?: CustomHttpResponseHandlers<ResponseType>)
    {
        const fullHeaders = this.getHeaders(useToken, headers)
        const observerObject = this.getObserverObject(handlers)

        const completeUrl = `${url}/${id}`
        this.http.delete<ResponseType>(completeUrl, {headers: fullHeaders}).subscribe(observerObject)
    }
}
