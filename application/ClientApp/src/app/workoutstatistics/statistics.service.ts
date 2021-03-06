import { Injectable } from '@angular/core';
import { sample } from 'rxjs/operators';
import { Sample } from '../model/workout/sample';
import { SampleX } from '../model/workout/samplex';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

  constructor() { }

  public convertToDist(samples: Sample[]): SampleX[] {
    let last: SampleX;
    return samples.map(x => {
      if (last) {
        const lastDist = last.x;
        const delta = this.dist(last.sample, x);
        last = new SampleX(x, lastDist + delta);
      } else {
        last = new SampleX(x, 0);
      }
      return last;
    });
  }

  public dist(sample1: Sample, sample2: Sample): number {
    const R = 6371000;
    const dLat = this.deg2rad(sample2.latitude - sample1.latitude);
    const dLon = this.deg2rad(sample2.longitude - sample1.longitude);
    const r = Math.sin(dLat / 2) *
      Math.sin(dLat / 2) +
      Math.cos(this.deg2rad(sample1.latitude)) *
      Math.cos(this.deg2rad(sample2.latitude)) *
      Math.sin(dLon / 2) *
      Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(r), Math.sqrt(1 - r));
    const d = R * c;
    return d;
  }

  public deg2rad(deg: number): number {
    return deg * Math.PI / 180;
  }

}
