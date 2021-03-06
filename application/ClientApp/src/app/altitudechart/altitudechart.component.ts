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

  public data = [
    {
      name: 'Altitude',
      series: [
        {
          name: 2010,
          value: 7300000
        }
      ]
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
  }
  public get samples(): AvgSampleX[] {
    return this._samples;
  }

  constructor(private statisticsService: StatisticsService) { }


  ngOnInit(): void {
  }

}
