import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MultiSeries } from '@swimlane/ngx-charts';
import { interval, Subscription } from 'rxjs';
import { AvgSampleX } from '../model/workout/avgsamplex';


@Component({
  selector: 'app-altitudechart',
  templateUrl: './altitudechart.component.html',
  styleUrls: ['./altitudechart.component.scss']
})
export class AltitudechartComponent implements OnInit, OnDestroy {

  public data: MultiSeries  = [
    {
      name: 'Altitude',
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
      return {name: x.dist, value: x.evelation};
    });

    this._minAltitude = Math.min(...this._samples.map(x => x.evelation));
    this._maxAltitude = Math.max(...this._samples.map(x => x.evelation));
  }
  public get samples(): AvgSampleX[] {
    return this._samples;
  }

  // tslint:disable-next-line:variable-name
  private _minAltitude = 0;

  public get minAltitude(): number {
    return this._minAltitude;
  }

  // tslint:disable-next-line:variable-name
  private _maxAltitude = 0;
  public get maxAltitude(): number {
    return this._maxAltitude;
  }

  // tslint:disable-next-line:variable-name
  private _chartWidth = 1000;

  public get chartWidth(): number {
    return this._chartWidth;
  }



  public get chartHeight(): number {
    const diff = this.maxAltitude - this.minAltitude;
    return Math.min(diff * 1.5, 700);
  }

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
