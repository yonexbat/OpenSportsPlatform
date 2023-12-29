import { Component } from '@angular/core';
import { ActivatedRoute, Params, } from '@angular/router';
import { icon, latLng, LatLngTuple, Layer, Marker, marker, Polyline, polyline, tileLayer } from 'leaflet';
import { timer } from 'rxjs';
import { Sample } from '../model/workout/sample';
import { Workout } from '../model/workout/workout';
import { getImageFromCategory, ticksToString } from '../util/util';
import { WorkoutService } from '../workout.service';



@Component({
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.scss']
})
export class WorkoutComponent {

  public workout?: Workout;
  public samples?: Sample[];
  public options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' })
    ],
    zoom: 5,
    center: latLng(46.879966, -121.726909)
  };

  private mapLink = '<a href="http://openstreetmap.org">OpenStreetMap</a>';
  private ocmlink = '<a href="http://thunderforest.com/">Thunderforest</a>';

  public layersControl = {
    baseLayers: {
      'Open Street Map': tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' }),
      'Open Cycle Map': tileLayer(
        'https://tile.thunderforest.com/cycle/{z}/{x}/{y}.png?apikey=8ae171ac1d904baea2ef1f3e9a7cf4e8', {
        attribution: `&copy; ${this.mapLink} Contributors ${this.ocmlink}`,
        maxZoom: 19,
      })
    },
    overlays: {},
  };

  public layers: Layer[] = [];

  // tslint:disable-next-line:variable-name
  private _sliderval: number | null = 0;

  public markerPosText = '00:00';


  constructor(
    private workoutService: WorkoutService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(x => this.handleRouteParamChanged(x));
  }

  public get sliderval(): number | null {
    return this._sliderval;
  }

  public set sliderval(val) {
    this._sliderval = val;
    this.updateMarker();
  }

  handleRouteParamChanged(params: Params): void {
    const  id: number = parseInt(params['id']);
    this.loadData(id);
  }

  async loadData(id: number): Promise<void> {    
    const workout = await this.workoutService.getWorkout(id);    
    this.workout = workout;

    // Track
    const polyLineWorkout = this.createPolyLine(this.workout);
    this.layers.push(polyLineWorkout);

    // marker
    const startMarker = this.createMarker(this.workout);
    timer(1).subscribe(() => startMarker.openPopup());
    this.layers.push(startMarker);

    if (this.workout.samples?.length > 0) {
      this.options.center = latLng(this.workout.samples[0].latitude, this.workout.samples[0].longitude);
      this.options.zoom = 15;
    }
  }

  private createPolyLine(workout: Workout): Polyline {
    const linedata = workout.samples.map(x => [x.latitude, x.longitude] as LatLngTuple);
    const polyLineWorkout = polyline(linedata);
    return polyLineWorkout;
  }

  private createMarker(workout: Workout): Marker {
    const iconRetinaUrl = 'assets/images/leaflet/marker-icon-2x.png';
    const iconUrl = 'assets/images/leaflet/marker-icon.png';
    const shadowUrl = 'assets/images/leaflet/marker-shadow.png';
    const iconDefault = icon({
      iconRetinaUrl,
      iconUrl,
      shadowUrl,
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41],
    });

    const makerLongLat = [workout.samples[0].latitude, workout.samples[0].longitude] as LatLngTuple;
    const startMarker = marker(makerLongLat, { title: `yuppi duppi`, icon: iconDefault });
    startMarker.bindPopup('00:00');
    return startMarker;
  }

  private updateMarker(): void {
    if (this.workout) {
      const val = this._sliderval || 0;
      const length = this.getTrainingLengthInMillis();
      const firstTime = this.getTimeOfLastSample();
      const currentPoint = (val / 10000) * length;
      const pointToShow = currentPoint + firstTime;

      const sample = this.getSample(pointToShow);

      const currentMarker = this.getMarker();
      const latLong = [sample?.latitude, sample?.longitude] as LatLngTuple;
      currentMarker.setLatLng(latLong);
      const tooltip = ticksToString(currentPoint);
      this.markerPosText = tooltip;
      currentMarker.setPopupContent(tooltip);
      currentMarker.options.title = tooltip;
    }
  }

  private getTrainingLengthInMillis(): number {
    if (this.workout) {
      const firstTime = this.getTimeOfLastSample();
      const lastTime = this.getTimeOfFirstSample();
      const length = lastTime - firstTime;
      return length;
    }
    return 0;
  }

  private getTimeOfLastSample(): number {
    if (this.workout) {
      const firstTimeString: string = this.workout.samples[0].timestamp;
      const firstTime = new Date(firstTimeString).getTime();
      return firstTime;
    }
    return 0;
  }

  private getTimeOfFirstSample(): number {
    if (this.workout) {
      const lastTimeString: string = this.workout.samples[this.workout?.samples.length - 1].timestamp;
      const lastTime = new Date(lastTimeString).getTime();
      return lastTime;
    }
    return 0;
  }

  private getMarker(): Marker {
    return this.layers[1] as Marker;
  }

  private getSample(pointToShow: number): Sample | undefined {
    const sample = this.workout?.samples.find(x => {
      const timeOfSample = new Date(x.timestamp).getTime();
      return timeOfSample >= pointToShow;
    });
    return sample;
  }

  public getImage(sportCat?: string): string {
    return getImageFromCategory(sportCat);
  }
}
