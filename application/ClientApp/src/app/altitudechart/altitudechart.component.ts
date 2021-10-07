import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
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
  }
  public get samples(): AvgSampleX[] {
    return this._samples;
  }

  public get minAltitude(): number {
    if (this.samples) {
      return Math.min(... this.samples.map(x => x.evelation));
    }
    return 0;
  }

  public get maxAltitude(): number {
    if (this.samples) {
      return Math.max(... this.samples.map(x => x.evelation));
    }
    return 0;
  }

  public get chartWidth(): number {
    return this.divElem.nativeElement.offsetWidth;
  }


  constructor(private statisticsService: StatisticsService) { }


  ngOnInit(): void {
  }

  public divResized(event: any): any {
    console.log(event);
  }

}
