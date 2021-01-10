import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedResult } from './model/common/pagedresult';
import { Workout } from './model/workout/workout';
import { WorkoutOverviewItem } from './model/workoutOverview/workoutOverviewItem';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private apiPrefix = '';

  constructor(private http: HttpClient) {

  }

  public getForeCast(): Promise<any> {
    return this.http.get('/WeatherForecast').toPromise();
  }

  public searchWorkoutItems(page: number): Promise<PagedResult<WorkoutOverviewItem>> {
    return this.http.get<PagedResult<WorkoutOverviewItem>>(`${this.apiPrefix}/Data/SearchWorkoutItems?Page=${page}`).toPromise();
  }

  public getWorkout(id: number): Promise<Workout> {
    return this.http.get<Workout>(`${this.apiPrefix}/Data/GetWorkout/${id}`).toPromise();
  }
}
