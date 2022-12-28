import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { filter, map, pairwise, tap, throttleTime } from 'rxjs/operators';
import { DataService } from '../data.service';
import { WorkoutOverviewItem } from '../model/workoutOverview/workoutOverviewItem';
import { getImageFromCategory } from '../util/util';


@Component({
  selector: 'app-workout-over-view2',
  templateUrl: './workout-over-view2.component.html',
  styleUrls: ['./workout-over-view2.component.scss']
})
export class WorkoutOverView2Component implements OnInit {

  public workoutItems: WorkoutOverviewItem[] = [];

  private currentPage = 0;

  @ViewChild('ScrollerRef', { static: true }) scroller!: CdkVirtualScrollViewport;

  constructor(private dataService: DataService, private ngZone: NgZone) {
    this.fetchMore();
  }

  ngOnInit(): void {

    const height = this.scroller.measureViewportSize('vertical');
    console.log(`viewport size is ${height}`);
    const numPages = height / 500.0;
    this.fetchPages(numPages);

    
    this.scroller.elementScrolled()
    .pipe(
      map(() => this.scroller.measureScrollOffset('bottom')),
      pairwise(),
      tap(([y1, y2]) =>  {
        console.log(`y1: ${y1}, y2: ${y2}`)
      }),
      filter(([y1, y2]) => (y2 < y1 && y2 < 300)),
      throttleTime(200)
    ).subscribe(() => {
      this.ngZone.run(() => {
        this.fetchMore();
      });
    });
  }

  private async fetchPages(numLoads: number): Promise<void> {
    while(numLoads > 0) {
      await this.fetchMore();
      numLoads --;
    }
  }

  public async fetchMore(): Promise<void> {
    this.currentPage++;
    const newItems = await this.dataService.searchWorkoutItems(this.currentPage - 1);
    this.workoutItems = [...this.workoutItems, ...newItems.data];
  }

  public getImage(sportCat: string): string {
    return getImageFromCategory(sportCat);
  }

}
