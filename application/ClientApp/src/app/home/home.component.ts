import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication.service';
import { DataService } from '../data.service';
import { ShortUserProfile } from '../model/shortUserProfile';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public userProfile$: Observable<ShortUserProfile>;

  constructor(private authService: AuthenticationService) {
    this.userProfile$ = authService.getUserProfile();
    authService.isLoggedInObservalbe();
  }

  ngOnInit(): void {
  }
}
