import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication.service';
import { ShortUserProfile } from '../model/shortUserProfile';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  public userProfile$: Observable<ShortUserProfile>;

  constructor(authService: AuthenticationService) {
    this.userProfile$ = authService.getUserProfile();
    authService.isLoggedInObservalbe();
  }

}
