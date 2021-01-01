import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { map, switchMap } from 'rxjs/operators';
import { BehaviorSubject, from, Observable, Subject } from 'rxjs';
import { ExchangeToken } from './model/exchangetoken';
import { ShortUserProfile } from './model/shortUserProfile';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private unauthenticatedUserProfile: ShortUserProfile  = {
    authenticated: false,
    name: '',
    userid: 'anonymous',
    roles: [],
  };

  private userProfile: Subject<ShortUserProfile> = new BehaviorSubject<ShortUserProfile>(this.unauthenticatedUserProfile);

  constructor(private http: HttpClient, private authService: SocialAuthService) {
    this.startUp();
  }

  public async signInGoogle(): Promise<void> {
    console.log('signing in to google');
    const user = await this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    await this.exchangeToken(user);
    await this.fetchUserProfile();
    console.log('done signing in');
  }

  public async signOut(): Promise<void> {
    localStorage.removeItem('jwt');
    await this.authService.signOut();
    this.userProfile.next(this.unauthenticatedUserProfile);
  }

  public getUserProfile(): Observable<ShortUserProfile> {
    return this.userProfile.asObservable();
  }

  public isUserLoggedIn(): Observable<boolean> {
    return this.getUserProfile().pipe(map(x => x.authenticated));
  }

  private async startUp(): Promise<void> {
    const jwt = localStorage.getItem('jwt');
    if (jwt) {
      await this.fetchUserProfile();
    }
  }

  private async fetchUserProfile(): Promise<void> {
    const profile = await this.http.get<ShortUserProfile>('/Authentication/GetShortUserProfile').toPromise();
    this.userProfile.next(profile);
  }

  private async exchangeToken(user: SocialUser): Promise<SocialUser> {
    const exchangeToken: ExchangeToken = {
      idToken: user.idToken,
    };
    const token = await this.http.post<string>('/Authentication/ExchangeToken', exchangeToken).toPromise();
    localStorage.setItem('jwt', token);
    return user;
  }
}