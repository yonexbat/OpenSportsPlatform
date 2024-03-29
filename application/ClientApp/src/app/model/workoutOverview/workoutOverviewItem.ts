export interface WorkoutOverviewItem {
    id?: number;
    startTime?: Date;
    endTime?: Date;
    sport?: string;
    distanceInKm: number;
    durationInSec: number;
}
