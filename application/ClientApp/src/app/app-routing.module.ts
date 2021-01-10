import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { WorkoutOverViewComponent } from './workout-over-view/workout-over-view.component';
import { WorkoutComponent } from './workout/workout.component';

const routes: Routes = [
  { path: '',   redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'workouts', component: WorkoutOverViewComponent, canActivate: [AuthGuard] },
  { path: 'workout/:id', component: WorkoutComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
