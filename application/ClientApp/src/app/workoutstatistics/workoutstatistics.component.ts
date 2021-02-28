import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ConfirmService } from '../confirm.service';
import { DataService } from '../data.service';
import { Workout } from '../model/workout/workout';
import { getImageFromCategory } from '../util/util';

@Component({
  selector: 'app-workoutstatistics',
  templateUrl: './workoutstatistics.component.html',
  styleUrls: ['./workoutstatistics.component.scss']
})
export class WorkoutstatisticsComponent implements OnInit {

  public workout?: Workout;

  public panelOpenState = true;

  constructor(
    private dataService: DataService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService) {
      this.route.params.subscribe(x => this.handleRouteParamChanged(x));
     }

  ngOnInit(): void {
  }

  handleRouteParamChanged(params: Params): void {
    const id = params.id;
    this.loadData(id);
  }

  async loadData(id: number): Promise<void> {
    this.workout = await this.dataService.getWorkout(id);
  }

  public getImage(sportCat?: string): string {
    return getImageFromCategory(sportCat);
  }
}
