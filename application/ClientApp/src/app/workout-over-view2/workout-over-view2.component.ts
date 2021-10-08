import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { filter, map, pairwise, throttleTime } from 'rxjs/operators';
import { DataService } from '../data.service';


@Component({
  selector: 'app-workout-over-view2',
  templateUrl: './workout-over-view2.component.html',
  styleUrls: ['./workout-over-view2.component.scss']
})
export class WorkoutOverView2Component implements OnInit {

  public arrNumber: number[] = [];

  @ViewChild('ScrollerRef', { static: true }) scroller!: CdkVirtualScrollViewport;

  constructor(private dataService: DataService) {
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
      console.log(x);
      this.fetchMore();
    });
  }

  private fetchMore(): void {

    for (let i = 0; i < 20; i++){
      this.arrNumber.push(i);
    }
    this.arrNumber = [...this.arrNumber];
  }

}
