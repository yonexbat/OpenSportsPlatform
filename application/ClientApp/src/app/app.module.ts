import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { UserInfoComponent } from './user-info/user-info.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { WorkoutOverViewComponent } from './workout-over-view/workout-over-view.component';
import {MatTableModule} from '@angular/material/table'; 
import { MatPaginatorModule } from '@angular/material/paginator';
import { HomeComponent } from './home/home.component';

// Use your Client ID in the GoogleLoginProvider()
const socialProviders = [
  { id: GoogleLoginProvider.PROVIDER_ID, provider: new GoogleLoginProvider('849271880529-rb5kdv2go744qt3uathulgd28id194cp.apps.googleusercontent.com') }
];


@NgModule({
  declarations: [
    AppComponent,
    UserInfoComponent,
    WorkoutOverViewComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SocialLoginModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: socialProviders,
      } as SocialAuthServiceConfig,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
