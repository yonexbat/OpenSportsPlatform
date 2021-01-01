import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WorkoutOverViewComponent } from './workout-over-view/workout-over-view.component';

const routes: Routes = [
  { path: '',   redirectTo: '/workouts', pathMatch: 'full' },
  { path: 'workouts', component: WorkoutOverViewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
