import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedResult } from './model/common/pagedresult';
import { WorkoutOverviewItem } from './model/WorkoutOverview/workoutOverviewItem';

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
}
