import { Injectable } from '@angular/core';
import {UserManager,User,UserManagerSettings} from 'oidc-client'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
private manager=new UserManager(this.getClientSettings());
private user: User = null;
  constructor() { 
    this.manager.getUser().then(user => {
      this.user = user;
  });

  }
   getClientSettings():UserManagerSettings{
    return{
      authority: 'https://localhost:44323/',
      client_id: 'openIdConnectClient',
      redirect_uri: 'http://localhost:4200/auth-callback',
      post_logout_redirect_uri: 'http://localhost:4200/',
      response_type:"id_token token",
      scope:"openid profile email role customAPI.write",
      filterProtocolClaims: true,
      loadUserInfo: true
    };
  }
  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
}
getClaims(): any {
  return this.user.profile;
}
getAuthorizationHeaderValue(): string {
  return `${this.user.token_type} ${this.user.access_token}`;
}
startAuthentication(): Promise<void> {
  return this.manager.signinRedirect();
}
completeAuthentication(): Promise<void> {
  return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
  });
}
}
