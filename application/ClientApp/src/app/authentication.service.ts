import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ExchangeToken } from './model/exchangetoken';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private authService: SocialAuthService) {}

  public async signInGoogle(): Promise<void> {
    console.log('signing in to google');
    const user = await this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    await this.exchangeToken(user);
    console.log('done signing in');
  }

  public authenticationState(): Observable<SocialUser> {
    return this.authService.authState.pipe(map(p => p));
  }

  private async exchangeToken(user: SocialUser): Promise<void> {
    console.log(user);
    const exchangeToken: ExchangeToken = {
      idToken: user.idToken,
    };
    const token = await this.http.post<string>('/Authentication/ExchangeToken', exchangeToken).toPromise();
    localStorage.setItem('jwt', token);
  }

}
