import { Component, Input, OnInit } from '@angular/core';
import { Workout } from '../model/workout/workout';

@Component({
  selector: 'app-altitudechart',
  templateUrl: './altitudechart.component.html',
  styleUrls: ['./altitudechart.component.scss']
})
export class AltitudechartComponent implements OnInit {

  public data = [
    {
      name: 'Germany',
      series: [
        {
          name: '2010',
          value: 7300000
        },
        {
          name: '2011',
          value: 8940000
        },
        {
          name: '2012',
          value: 6940000
        },
        {
          name: '2013',
          value: 8940000
        }
      ]
    }
  ];

  constructor() { }

  @Input() workout?: Workout;

  ngOnInit(): void {
  }

}
