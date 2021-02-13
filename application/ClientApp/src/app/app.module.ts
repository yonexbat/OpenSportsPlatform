import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { WorkoutOverViewComponent } from './workout-over-view/workout-over-view.component';
import { MatTableModule } from '@angular/material/table';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';
import { HomeComponent } from './home/home.component';
import { NavigationComponent } from './navigation/navigation.component';
import { WorkoutComponent } from './workout/workout.component';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { UploadWorkoutComponent } from './upload-workout/upload-workout.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { StatsComponent } from './stats/stats.component';

// Use your Client ID in the GoogleLoginProvider()
const socialProviders = [
  { id: GoogleLoginProvider.PROVIDER_ID, provider: new GoogleLoginProvider('849271880529-rb5kdv2go744qt3uathulgd28id194cp.apps.googleusercontent.com') }
];


@NgModule({
  declarations: [
    AppComponent,
    WorkoutOverViewComponent,
    HomeComponent,
    NavigationComponent,
    WorkoutComponent,
    FileUploaderComponent,
    UploadWorkoutComponent,
    ConfirmComponent,
    StatsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatPaginatorModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatProgressBarModule,
    MatDialogModule,
    MatCardModule,
    MatTooltipModule,
    LeafletModule,
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
