import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { interval } from 'rxjs';
import { AvgSampleX } from '../model/workout/avgsamplex';
import { StatisticsService } from '../workoutstatistics/statistics.service';

type AltitudeData = {name: string, series: {name: number, value: number}[]};

@Component({
  selector: 'app-altitudechart',
  templateUrl: './altitudechart.component.html',
  styleUrls: ['./altitudechart.component.scss']
})
export class AltitudechartComponent implements OnInit {

  public data: AltitudeData[]  = [
    {
      name: 'Altitude',
      series: []
    },
  ];


  // tslint:disable-next-line:variable-name
  private _samples: AvgSampleX[] = [];

  @ViewChild('ContainerRef', { static: true }) divElem!: ElementRef;

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
    return diff * 1;
  }


  constructor(private statisticsService: StatisticsService) { }


  ngOnInit(): void {
    this._chartWidth = this.divElem.nativeElement.offsetWidth;
    interval(500).subscribe(() => {
      this._chartWidth = this.divElem.nativeElement.offsetWidth;
    });
  }

  public divResized(event: any): any {
    console.log(event);
  }

}
