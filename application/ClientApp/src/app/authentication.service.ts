import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, shareReplay } from 'rxjs/operators';
import { BehaviorSubject, firstValueFrom, Observable, Subject } from 'rxjs';
import { ExchangeToken } from './model/exchangetoken';
import { ShortUserProfile } from './model/shortUserProfile';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private unauthenticatedUserProfile: ShortUserProfile = {
    authenticated: false,
    name: '',
    userid: 'anonymous',
    roles: [],
  };

  private userProfile: Subject<ShortUserProfile> = new BehaviorSubject<ShortUserProfile>(this.unauthenticatedUserProfile);
  //private userProfileReplay?: Observable<ShortUserProfile> = undefined;

  constructor(private http: HttpClient, private authService: SocialAuthService) {

    this.startUp();
  }

  public async signOut(): Promise<void> {
    localStorage.removeItem('jwt');
    await this.authService.signOut();
    //this.userProfileReplay = undefined;
    this.userProfile.next(this.unauthenticatedUserProfile);
  }

  public getUserProfile(): Observable<ShortUserProfile> {
    return this.userProfile.asObservable();
  }

  public isLoggedInObservalbe(): Observable<boolean> {
    return this.getUserProfile()
      .pipe(map((shortUserProfile: ShortUserProfile) => shortUserProfile.authenticated));
  }

  public async isLoggedIn(): Promise<boolean> {
    const userProfile = await firstValueFrom(this.userProfile);
    return userProfile.authenticated === true;
  }

  private async startUp(): Promise<void> {
    const jwt = localStorage.getItem('jwt');
    if (jwt) {
      await this.fetchUserProfile();
    }

    this.authService.authState.subscribe((user: SocialUser) => {
      this.signInIfNeeded(user);
    });
  }

  private async signInIfNeeded(user: SocialUser): Promise<void> {
    if (!user) {
      return;
    }
    const loggedIn = await this.isLoggedIn();
    if (!loggedIn) {
      await this.exchangeToken(user);
      await this.fetchUserProfile();
    }
  }

  private async fetchUserProfile(): Promise<ShortUserProfile> {
    const userProfile = await firstValueFrom(this.http.get<ShortUserProfile>('/Authentication/GetShortUserProfile'));
    this.userProfile.next(userProfile);
    return userProfile;
  }

  private async exchangeToken(user: SocialUser): Promise<SocialUser> {
    const exchangeToken: ExchangeToken = {
      idToken: user.idToken,
    };
    const token = await firstValueFrom(this.http.post<string>('/Authentication/ExchangeToken', exchangeToken)) as string;
    localStorage.setItem('jwt', token);
    return user;
  }
}
