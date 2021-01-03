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
  public showLoginSpinner = false;

  constructor(private authenticationService: AuthenticationService) {
    this.userProfile$ = authenticationService.getUserProfile();
  }

  ngOnInit(): void {
  }

  public async signIn(): Promise<void> {
    this.showLoginSpinner = true;
    await this.authenticationService.signInGoogle();
    this.showLoginSpinner = false;
  }

  public signOut(): void {
    this.authenticationService.signOut();
  }
}
