import { Component, Input, OnInit } from '@angular/core';
import { AvgSampleX } from '../model/workout/avgsamplex';
import { Workout } from '../model/workout/workout';
import { StatisticsService } from '../workoutstatistics/statistics.service';

@Component({
  selector: 'app-altitudechart',
  templateUrl: './altitudechart.component.html',
  styleUrls: ['./altitudechart.component.scss']
})
export class AltitudechartComponent implements OnInit {

  public data: {name: string, series: {name: any, value: any}[] }[]  = [
    {
      name: 'Altitude',
      series: []
    },
    {
      name: 'Heartrate',
      series: []
    }
  ];

  // tslint:disable-next-line:variable-name
  private _samples: AvgSampleX[] = [];

  @Input()
  public set samples(val: AvgSampleX[]) {
    this._samples = val;

    this.data[0].series = val.map(x => {
      return {name: x.dist, value: x.evelation};
    });

    this.data[1].series = val.map(x => {
      return {name: x.dist, value: x.heartRate};
    });


  }
  public get samples(): AvgSampleX[] {
    return this._samples;
  }

  constructor(private statisticsService: StatisticsService) { }


  ngOnInit(): void {
  }

}
