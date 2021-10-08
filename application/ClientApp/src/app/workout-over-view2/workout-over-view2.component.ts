import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { ChangeDetectorRef, Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { filter, map, pairwise, throttleTime } from 'rxjs/operators';
import { DataService } from '../data.service';
import { getImageFromCategory } from '../util/util';


@Component({
  selector: 'app-workout-over-view2',
  templateUrl: './workout-over-view2.component.html',
  styleUrls: ['./workout-over-view2.component.scss']
})
export class WorkoutOverView2Component implements OnInit {

  public workoutItems: any[] = [];

  private currentPage = 0;

  @ViewChild('ScrollerRef', { static: true }) scroller!: CdkVirtualScrollViewport;

  constructor(private dataService: DataService, private ngZone: NgZone) {
    this.fetchMore();
   }

  ngOnInit(): void {

    this.scroller.elementScrolled()
      .pipe(
        map( () => this.scroller.measureScrollOffset('bottom')),
        pairwise(),
        filter(([y1, y2]) => (y2 < y1 && y2 < 300)),
        throttleTime(200)
      ).subscribe(x => {
        this.ngZone.run(() => {
          this.fetchMore();
        });
    });
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
