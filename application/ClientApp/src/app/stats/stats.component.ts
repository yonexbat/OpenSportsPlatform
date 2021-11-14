import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Color, MultiSeries, ScaleType, SingleSeries } from '@swimlane/ngx-charts';
import { interval, Subscription } from 'rxjs';
import { DataService } from '../data.service';
import { Statistics } from '../model/statistics/statistics';
import { getImageFromCategory } from '../util/util';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit, OnDestroy {

  public statistics: Statistics = {
    monthItems: [],
    yearItems: [],
    runningLast12Months: [],
  };

  public data: SingleSeries = [
  ];

  public displayedColumns: string[] = ['sportsCategoryIcon', 'sportsCategoryName', 'distanceInKm'];

  public color: Color = {
    name: 'blue',
    group: ScaleType.Linear,
    selectable: false,
    domain: ['#003c88'], 
  };

  @ViewChild('RunningYearRef', { static: true }) divElem!: ElementRef;

  private intervalSub?: Subscription = undefined;

  // tslint:disable-next-line:variable-name
  private _chartWidth = 0;

  public get chartWidth(): number {
    return this._chartWidth;
  }

  // tslint:disable-next-line:variable-name
  private _chartHeight = 400;

  public get chartHeight(): number {
    return this._chartHeight;
  }


  constructor(private dataService: DataService) { }
  ngOnDestroy(): void {
    if (this.intervalSub) {
      this.intervalSub.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.load();
    this.intervalSub = interval(500).subscribe(() => {
      this._chartWidth = this.divElem.nativeElement.offsetWidth;
    });
  }

  private async load(): Promise<void> {
    this.statistics = await this.dataService.getStatistics();
    this.data = this.statistics.runningLast12Months.reduce((previous, current) => {
      previous.push({
        name: `${current.year}.${current.month}`,
        value: current.value,
        //label: `${current.year}.${current.month} km`
      });
      return previous;
    }, [] as SingleSeries);
  }

  public getImage(sportCat: string): string {
    return getImageFromCategory(sportCat);
  }

}
