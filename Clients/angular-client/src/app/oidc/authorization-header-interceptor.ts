import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { OpenIdConnectService } from './open-id-connect.service';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
//http Requset拦截器
//用于拦截已登录用户的request请求，将授权方式和accessToken写入request中再发送出去
//需要再NgModule的Provider中注册才能使用
export class AuthorizationHeaderInterceptor implements HttpInterceptor {
    constructor(private oidc: OpenIdConnectService) { }

    intercept(request:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
        if (this.oidc.userAvailable) {
            request = request.clone({
                setHeaders:{
                    Authorization:`${this.oidc.user.token_type} ${this.oidc.user.access_token}`
                }
            });
        }

        return next.handle(request);
    }
}