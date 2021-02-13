import { Sample } from './sample';

export interface Workout {
    id?: number;
    startTime?: Date;
    endTime?: Date;
    sport?: string;
    samples: Sample[];
}