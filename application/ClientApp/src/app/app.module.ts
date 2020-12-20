import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule  } from 'angularx-social-login';

// Use your Client ID in the GoogleLoginProvider()
const socialProviders = [
    {id: GoogleLoginProvider.PROVIDER_ID, provider: new GoogleLoginProvider('849271880529-rb5kdv2go744qt3uathulgd28id194cp.apps.googleusercontent.com')}
];


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SocialLoginModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: socialProviders,
      } as SocialAuthServiceConfig,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
