import { identifierModuleUrl } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { latLng, tileLayer } from 'leaflet';
import { DataService } from '../data.service';
import { Workout } from '../model/workout/workout';

@Component({
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.scss']
})
export class WorkoutComponent implements OnInit {

  public workout?: Workout;

  public options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' })
    ],
    zoom: 5,
    center: latLng(46.879966, -121.726909)
  };

  constructor(private dataService: DataService, private route: ActivatedRoute) {
    this.route.params.subscribe(x => this.handleRouteParamChanged(x));
  }

  ngOnInit(): void {
  }

  handleRouteParamChanged(params: Params): void {
    const id = params.id;
    this.loadData(id);
  }

  async loadData(id: number): Promise<void> {
    this.workout = await this.dataService.getWorkout(id);
  }
}
