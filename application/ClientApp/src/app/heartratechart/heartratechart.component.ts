import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MultiSeries } from '@swimlane/ngx-charts';
import { interval, Subscription } from 'rxjs';
import { AvgSampleX } from '../model/workout/avgsamplex';


@Component({
  selector: 'app-heartratechart',
  templateUrl: './heartratechart.component.html',
  styleUrls: ['./heartratechart.component.scss']
})
export class HeartratechartComponent implements OnInit, OnDestroy {

  public data: MultiSeries = [
    {
      name: 'Heartrate',
      series: []
    },
  ];

  @ViewChild('ContainerRef', { static: true }) divElem!: ElementRef;

  private intervalSub?: Subscription = undefined;


  // tslint:disable-next-line:variable-name
  private _samples: AvgSampleX[] = [];

  @Input()
  public set samples(val: AvgSampleX[]) {
    this._samples = val;

    this.data[0].series = val.map(x => {
      return { name: x.dist, value: x.heartRate };
    });

    this._minSample = Math.min(...this._samples.map(x => x.heartRate));
    this._maxSample = Math.max(...this._samples.map(x => x.heartRate));
  }
  public get samples(): AvgSampleX[] {
    return this._samples;
  }

  // tslint:disable-next-line:variable-name
  private _minSample = 0;

  public get minSample(): number {
    return this._minSample;
  }

  // tslint:disable-next-line:variable-name
  private _maxSample = 0;
  public get maxSample(): number {
    return this._maxSample;
  }

  // tslint:disable-next-line:variable-name
  private _chartWidth = 1000;

  public get chartWidth(): number {
    return this._chartWidth;
  }

  public get chartHeight(): number {
    const diff = this.maxSample - this.minSample;
    return Math.min(diff * 10, 700);
  }

  constructor() { }

  ngOnDestroy(): void {
    if (this.intervalSub) {
      this.intervalSub.unsubscribe();
    }
  }

  ngOnInit(): void {
    this._chartWidth = this.divElem.nativeElement.offsetWidth;
    this.intervalSub = interval(500).subscribe(() => {
      this._chartWidth = this.divElem.nativeElement.offsetWidth;
    });
  }
}
