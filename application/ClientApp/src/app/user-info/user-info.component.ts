import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication.service';
import { ShortUserProfile } from '../model/shortUserProfile';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  public userProfile$: Observable<ShortUserProfile>;

  constructor(private authenticationService: AuthenticationService) {
    this.userProfile$ = authenticationService.getUserProfile();
  }

  ngOnInit(): void {
  }

  public signIn(): void {
    this.authenticationService.signInGoogle();
  }

  public signOut(): void {
    this.authenticationService.signOut();
  }
}
