import { ThrowStmt } from '@angular/compiler';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { from, merge } from 'rxjs';
import { map, startWith, switchMap } from 'rxjs/operators';
import { threadId } from 'worker_threads';
import { DataService } from '../data.service';
import { WorkoutOverviewItem } from '../model/workoutOverview/workoutOverviewItem';

@Component({
  selector: 'app-workout-over-view',
  templateUrl: './workout-over-view.component.html',
  styleUrls: ['./workout-over-view.component.scss']
})
export class WorkoutOverViewComponent implements OnInit, AfterViewInit  {

  public workouts: WorkoutOverviewItem[] = [];
  public count = 0;
  public displayedColumns: string[] = ['date', 'starttime', 'endtime', 'sport'];

  @ViewChild(MatPaginator) paginator: any;

  @ViewChild(MatSort) sort: any;

  constructor(private dataService: DataService) {
  }

  ngAfterViewInit(): void {
    const paginator = this.paginator as MatPaginator;
    merge(paginator.page).pipe(
      startWith({}),
      switchMap(x => {
        const page = paginator.pageIndex;
        const obsv = from(this.dataService.searchWorkoutItems(page));
        return obsv;
      })
    ).subscribe(x => {
      console.log(x);
      this.workouts = x.data;
      this.count = x.count;
    });
  }

  ngOnInit(): void {
  }

  itemClick(): void {
    console.log('item click');
  }

}
