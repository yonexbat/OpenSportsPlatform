import { Injectable } from '@angular/core';
import { AvgSampleX } from '../model/workout/avgsamplex';
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
        const lastDist = last.sumDist;
        const delta = this.dist(last.sample, x);
        last = new SampleX(x, lastDist + delta);
      } else {
        last = new SampleX(x, 0);
      }
      return last;
    });
  }

  public thinOut(samples: SampleX[], deltaDist: number): AvgSampleX[] {

    const map: Map<number, SampleX[]> = new Map<number, SampleX[]>();

    for (const sample of samples) {
      const currentDist = sample.sumDist;
      const currentKey = Math.floor(currentDist / deltaDist) * deltaDist;

      if (!map.has(currentKey)) {
        map.set(currentKey, []);
      }
      map.get(currentKey)?.push(sample);
    }

    const lastSample = samples[samples.length - 1];
    const lastKey = Math.floor(lastSample.sumDist / deltaDist) * deltaDist;

    const res: AvgSampleX[] = [];
    for (let key = 0; key <= lastKey; key += deltaDist) {
      if (map.has(key)) {
        const values = map.get(key);
        const numSamples = values?.length ?? 1;

        const sumEvelation = values?.reduce((accumulator, value)  => accumulator + value?.sample?.altitudeInMeters, 0) ?? 0;
        const averageEvelation = sumEvelation / numSamples;

        const sumHeartRate = values?.reduce((accumulator, value)  => accumulator + value?.sample?.heartRateBpm, 0) ?? 0;
        const averageHeartRate = sumHeartRate / numSamples;

        res.push(new AvgSampleX(averageEvelation ?? 0, averageHeartRate ?? 0, key));
      }
      else {
        const lastRes = res[res.length - 1];
        res.push(new AvgSampleX(lastRes.evelation, lastRes.heartRate, key));
      }
    }

    return res;
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
