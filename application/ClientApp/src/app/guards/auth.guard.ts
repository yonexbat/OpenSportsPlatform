import { Injectable } from '@angular/core';

import { from, Observable } from 'rxjs';
import { AuthenticationService } from '../authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {

  constructor(private authService: AuthenticationService) {
  }

  canActivate(): Observable<boolean> {
    return from(this.authService.isLoggedIn());
  }
}
