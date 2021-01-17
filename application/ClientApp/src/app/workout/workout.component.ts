import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { circle, latLng, LatLngTuple, marker, polygon, polyline, tileLayer } from 'leaflet';
import { ConfirmService } from '../confirm.service';
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

  public layersControl = {
    baseLayers: {
      'Open Street Map': tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' }),
      'Open Cycle Map': tileLayer('http://{s}.tile.opencyclemap.org/cycle/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' })
    },
    overlays: {},
  };

  public layers: any[] = [];


  constructor(
    private dataService: DataService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService
  ) {
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
    const linedata = this.workout.samples.map(x => [x.latitude, x.longitude] as LatLngTuple);
    const polyLineWorkout = polyline(linedata);
    this.layers.push(polyLineWorkout);

    if (this.workout.samples.length > 0) {
      this.options.center = latLng(this.workout.samples[0].latitude, this.workout.samples[0].longitude);
      this.options.zoom = 15;
    }
  }

  public deleteClick(): void {
    this.confirmService.confirm('Delete workout', 'Do you really want to delete this workout?')
      .subscribe(x => {
        if (x) {
          this.deleteWorkout();
        }
      });
  }

  private async deleteWorkout(): Promise<void> {
    if (this.workout && this.workout.id) {
      await  this.dataService.deleteWorkout(this.workout?.id);
      this.router.navigate(['workouts']);
    }
  }
}
