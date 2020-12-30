import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { WorkoutOverviewItem } from '../model/WorkoutOverview/workoutOverviewItem';

@Component({
  selector: 'app-workout-over-view',
  templateUrl: './workout-over-view.component.html',
  styleUrls: ['./workout-over-view.component.scss']
})
export class WorkoutOverViewComponent implements OnInit {

  public workouts: WorkoutOverviewItem[] = [];
  displayedColumns: string[] = ['starttime', 'endtime'];

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.loadWorkouts();
  }

  async loadWorkouts(): Promise<void> {
    this.workouts = await this.dataService.searchWorkoutItems(0);
  }

}
