import { TestBed } from '@angular/core/testing';
import { Workout } from '../model/workout/workout';
import { StatisticsService } from './statistics.service';
import * as workoutjson from './workouttest.json';

const workout: Workout = (workoutjson as {default: Workout}).default;

describe('StatisticsService', () => {
  let service: StatisticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StatisticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should generate distance data', () => {
    const res = service.convertToDist(workout.samples);
    expect(res).toBeTruthy();
    const last = res[res.length - 1];
    const dist = last.sumDist;
    expect(dist).toBeGreaterThan(12200);
  });

  it('should should thin out', () => {
    const samplesX = service.convertToDist(workout.samples);
    service.thinOut(samplesX, 500);
  });

});



