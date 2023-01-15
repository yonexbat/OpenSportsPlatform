import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { from } from 'rxjs';
import { startWith, switchMap, take } from 'rxjs/operators';
import { DataService } from '../data.service';
import { WorkoutOverviewItem } from '../model/workoutOverview/workoutOverviewItem';
import { getImageFromCategory } from '../util/util';

@Component({
  selector: 'app-workout-over-view',
  templateUrl: './workout-over-view.component.html',
  styleUrls: ['./workout-over-view.component.scss']
})
export class WorkoutOverViewComponent implements OnInit, AfterViewInit  {

  constructor(
    private dataService: DataService,
    private router: Router,
    private route: ActivatedRoute) {
  }

  public workouts: WorkoutOverviewItem[] = [];
  public count = 0;
  public displayedColumns: string[] = ['date', 'starttime', 'duration', 'distance', 'sport'];
  public pageIndex = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  @ViewChild(MatSort) sort: MatSort | undefined;

  public getImage(sportCat: string): string {
    return getImageFromCategory(sportCat);
  }

  ngAfterViewInit(): void {
    const paginator = this.paginator as MatPaginator;
    paginator.page.pipe(
      startWith({}),
      switchMap(() => {
        const page = paginator.pageIndex;
        const obsv = from(this.dataService.searchWorkoutItems(page));
        return obsv;
      })
    ).subscribe(x => {
      this.workouts = x.data;
      this.count = x.count;
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: { page: `${paginator.pageIndex}`},
        queryParamsHandling: 'merge',
        skipLocationChange: false,
      });
    });
  }

  ngOnInit(): void {
    this.route.queryParams.pipe(take(1))
    .subscribe((params: Params) => {
      const page = params['page'];
      const pageNumber = parseInt(page, 10);
      if (isNaN(pageNumber) === false) {
        this.pageIndex = pageNumber;
      }
    });
  }
}
