import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from '../open-id-connect.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.scss']
})
export class SigninOidcComponent implements OnInit {

  constructor(private oidc:OpenIdConnectService,private router:Router) { }

  ngOnInit() {
    this.oidc.userLoaded$.subscribe(userloaded =>{
      if(userloaded){
        this.router.navigate(["/"]);
      }else{
        console.log("error login");
      }
    });
    this.oidc.handleCallback();
  }

}
