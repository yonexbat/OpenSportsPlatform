import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DataService } from '../data.service';
import { AvgSampleX } from '../model/workout/avgsamplex';
import { Workout } from '../model/workout/workout';
import { getImageFromCategory } from '../util/util';
import { StatisticsService } from './statistics.service';

@Component({
  selector: 'app-workoutstatistics',
  templateUrl: './workoutstatistics.component.html',
  styleUrls: ['./workoutstatistics.component.scss']
})
export class WorkoutstatisticsComponent {

  public workout?: Workout;

  public panelOpenState = true;

  public samples: AvgSampleX[] = [];

  constructor(
    private dataService: DataService,
    private activatedRoute: ActivatedRoute,
    private statisticsService: StatisticsService) {
    this.activatedRoute.params.subscribe(params => this.handleRouteParamChanged(params));
  }

  handleRouteParamChanged(params: Params): void {
    const id = params['id'];
    this.loadData(id);
  }

  async loadData(id: number): Promise<void> {
    this.workout = await this.dataService.getWorkout(id);
    const dists = this.statisticsService.convertToDist(this.workout.samples);
    this.samples = this.statisticsService.thinOut(dists, 50);
  }

  public getImage(sportCat?: string): string {
    return getImageFromCategory(sportCat);
  }
}
