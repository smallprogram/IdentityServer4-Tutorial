import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client'
import { environment } from 'src/environments/environment';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {
  //oidc 用户管理器
  private userManager = new UserManager(environment.openIdConnectSettings);
  //当前用户
  private currentUser: User;
  //判断当前用户是否登录
  get userAvailable(): boolean {
    return !!this.currentUser;
  }
  //获取当前用户
  get user(): User {
    return this.currentUser;
  }

  userLoaded$ = new ReplaySubject<boolean>(1);

  constructor() {
    //清理之前登录痕迹，清理过期session或者localstore
    this.userManager.clearStaleState();

    
    //判断用户是否登录
    this.userManager.getUser().then(user => {
      if (user) {
        this.currentUser = user;
        this.userLoaded$.next(true);
      } else {
        this.currentUser = null;
        this.userLoaded$.next(false);
      }
    }).catch(err => {
      this.currentUser = null;
      this.userLoaded$.next(false);
    });

    //当用户加载时的事件触发
    this.userManager.events.addUserLoaded(user => {
      console.log('user loaded:', user);
      this.currentUser = user;
      this.userLoaded$.next(true);
    });
    //当用户未加载时的事件触发
    this.userManager.events.addUserUnloaded(user => {
      console.log('user unloaded');
      this.currentUser = null;
      this.userLoaded$.next(false);
    });
  }


  triggerSignIn() {
    this.userManager.signinRedirect().then(() => {
      console.log('triggerSignIn');
    });
  }

  handleCallback() {
    this.userManager.signinRedirectCallback().then(user => {
      this.currentUser = user;
      console.log('handleCallback');
    });
  }

  handleSilentCallback() {
    this.userManager.signinSilentCallback().then(user => {
      this.currentUser = user;
      console.log('handleSilentCallback');
    });
  }

  triggerSignOut() {
    this.userManager.signoutRedirect().then(res => {
      console.log('triggerSignOut');
    });
  }
}
