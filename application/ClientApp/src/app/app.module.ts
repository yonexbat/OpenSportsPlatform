import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
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
import { MatInputModule } from '@angular/material/input';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSliderModule } from '@angular/material/slider';
import { HomeComponent } from './home/home.component';
import { NavigationComponent } from './navigation/navigation.component';
import { WorkoutComponent } from './workout/workout.component';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { UploadWorkoutComponent } from './upload-workout/upload-workout.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { StatsComponent } from './stats/stats.component';
import { GlobalErrorHandler } from './errorhandler/globalerrorhandler';
import { EditworkoutComponent } from './editworkout/editworkout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AltitudechartComponent } from './altitudechart/altitudechart.component';
import { TabmenuComponent } from './tabmenu/tabmenu.component';
import { WorkoutstatisticsComponent } from './workoutstatistics/workoutstatistics.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DistancePipe } from './pipes/distance.pipe';
import { DurationPipe } from './pipes/duration.pipe';
import { CountdownComponent } from './countdown/countdown.component';
import { SyncComponent } from './sync/sync.component';
import { HeartratechartComponent } from './heartratechart/heartratechart.component';
import { WorkoutOverView2Component } from './workout-over-view2/workout-over-view2.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PolarComponent } from './polar/polar.component';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule, GoogleSigninButtonModule } from '@abacritt/angularx-social-login';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';

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
    EditworkoutComponent,
    AltitudechartComponent,
    TabmenuComponent,
    WorkoutstatisticsComponent,
    DistancePipe,
    DurationPipe,
    CountdownComponent,
    SyncComponent,
    HeartratechartComponent,
    WorkoutOverView2Component,
    PolarComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    GoogleSigninButtonModule,
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
    MatInputModule,
    MatExpansionModule,
    MatTabsModule,
    MatSliderModule,
    LeafletModule,
    ReactiveFormsModule,
    FormsModule,
    NgxChartsModule,
    MatSnackBarModule,
    ScrollingModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: socialProviders,
      } as SocialAuthServiceConfig,
    },
    {
      // processes all errors
      provide: ErrorHandler,
      useClass: GlobalErrorHandler,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
