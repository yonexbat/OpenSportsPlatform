import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditworkoutComponent } from './editworkout/editworkout.component';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { StatsComponent } from './stats/stats.component';
import { TabmenuComponent } from './tabmenu/tabmenu.component';
import { UploadWorkoutComponent } from './upload-workout/upload-workout.component';
import { WorkoutOverViewComponent } from './workout-over-view/workout-over-view.component';
import { WorkoutComponent } from './workout/workout.component';
import { WorkoutstatisticsComponent } from './workoutstatistics/workoutstatistics.component';

const routes: Routes = [
  { path: '',   redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'workouts', component: WorkoutOverViewComponent, canActivate: [AuthGuard] },
  { path: 'uploadworkout', component: UploadWorkoutComponent, canActivate: [AuthGuard] },
  { path: 'test', component: FileUploaderComponent, canActivate: [AuthGuard] },

  {
    path: 'workout',
    component: TabmenuComponent,
    canActivate: [AuthGuard],
    children: [
      {path: 'map/:id', component: WorkoutComponent},
      {path: 'stats/:id', component: WorkoutstatisticsComponent},
      {path: 'editworkout/:id', component: EditworkoutComponent},
    ]
   },
  { path: 'statistics', component: StatsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
