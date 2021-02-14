import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedResult } from './model/common/pagedresult';
import { EditWorkout } from './model/editworkout/editWorkout';
import { SaveWorkout } from './model/editworkout/saveWorkout';
import { Statistics } from './model/statistics/statistics';
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

  public getEditWorkout(id: number): Promise<EditWorkout> {
    return this.http.get<EditWorkout>(`${this.apiPrefix}/Data/GetEditWorkout/${id}`).toPromise();
  }

  public saveWorkout(dto: SaveWorkout): Promise<boolean> {
    return this.http.post<boolean>(`${this.apiPrefix}/Data/SaveWorkout`, dto).toPromise();
  }

  public deleteWorkout(id: number): Promise<boolean> {
    return this.http.delete<boolean>(`${this.apiPrefix}/Data/DeleteWorkout/${id}`).toPromise();
  }

  public getStatistics(): Promise<Statistics> {
    return this.http.get<Statistics>(`${this.apiPrefix}/Data/GetStatistics`).toPromise();
  }
}
