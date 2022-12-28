import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication.service';
import { ShortUserProfile } from '../model/shortUserProfile';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {

  public isLoggedIn$: Observable<boolean>;
  public userProfile$: Observable<ShortUserProfile>;
  public showLoginSpinner = false;

  constructor(private authenticationService: AuthenticationService) {
    this.isLoggedIn$ = authenticationService.isLoggedInObservalbe();
    this.userProfile$ = authenticationService.getUserProfile();
  }

  public signOut(): void {
    this.authenticationService.signOut();
  }

  public throwTestError(): void {
    throw new Error('Testerror');
  }

}
