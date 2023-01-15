import { Sample } from './sample';

export interface Workout {
    id?: number;
    startTime?: Date;
    endTime?: Date;
    sport?: string;
    samples: Sample[];
    distanceInKm?: number;
    durationInSec?: number;
    caloriesInKCal?: number;
    ascendInMeters?: number;
    descendInMeters?: number;
    heartRateAvgBpm?: number;
    heartRateMaxBpm?: number;
}
