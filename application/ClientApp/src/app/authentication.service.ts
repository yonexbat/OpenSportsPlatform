import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private authService: SocialAuthService) {}

  public async login(): Promise<any> {
    const token = await this.http.get<string>('/Authentication/JwtToken').toPromise();
    localStorage.setItem('jwt', token);
  }

  public signInGoogle(): void {
    console.log('signing in to google');
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    console.log('done signing in');
  }

  public authenticationState(): Observable<SocialUser> {
    return this.authService.authState.pipe(map(p => p));
  }

}
