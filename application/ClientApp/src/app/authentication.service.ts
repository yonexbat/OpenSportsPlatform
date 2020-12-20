import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { map, switchMap } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { ExchangeToken } from './model/exchangetoken';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private authService: SocialAuthService) {}

  public async signInGoogle(): Promise<void> {
    console.log('signing in to google');
    const user = await this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    console.log('done signing in');
  }

  public authenticationState(): Observable<SocialUser> {
    return this.authService.authState.pipe(switchMap(p => from(this.exchangeToken(p))));
  }


  private async exchangeToken(user: SocialUser): Promise<SocialUser> {
    console.log(user);
    const exchangeToken: ExchangeToken = {
      idToken: user.idToken,
    };
    const token = await this.http.post<string>('/Authentication/ExchangeToken', exchangeToken).toPromise();
    localStorage.setItem('jwt', token);
    return user;
  }

}
