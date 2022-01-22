import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { PagedResult } from './model/common/pagedresult';
import { CropWorkout } from './model/editworkout/cropWorkout';
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

  public searchWorkoutItems(page: number): Promise<PagedResult<WorkoutOverviewItem>> {
    return firstValueFrom(this.http.get<PagedResult<WorkoutOverviewItem>>(`${this.apiPrefix}/Data/SearchWorkoutItems?Page=${page}`)) as Promise<PagedResult<WorkoutOverviewItem>>;
  }

  public getWorkout(id: number): Promise<Workout> {
    return firstValueFrom(this.http.get<Workout>(`${this.apiPrefix}/Data/GetWorkout/${id}`)) as Promise<Workout>;
  }

  public getEditWorkout(id: number): Promise<EditWorkout> {
    return firstValueFrom(this.http.get<EditWorkout>(`${this.apiPrefix}/Data/GetEditWorkout/${id}`)) as Promise<EditWorkout>;
  }

  public saveWorkout(dto: SaveWorkout): Promise<boolean> {
    return firstValueFrom(this.http.post<boolean>(`${this.apiPrefix}/Data/SaveWorkout`, dto)) as Promise<boolean> ;
  }

  public deleteWorkout(id: number): Promise<boolean> {
    return firstValueFrom(this.http.delete<boolean>(`${this.apiPrefix}/Data/DeleteWorkout/${id}`)) as Promise<boolean>;
  }

  public getStatistics(): Promise<Statistics> {
    return firstValueFrom(this.http.get<Statistics>(`${this.apiPrefix}/Data/GetStatistics`)) as Promise<Statistics>;
  }

  public syncPolar(): Promise<any> {
    return firstValueFrom(this.http.post<boolean>(`${this.apiPrefix}/Data/SyncPolar`, {})) as Promise<any>;
  }

  public crop(dto: CropWorkout): Promise<any> {
    return firstValueFrom(this.http.post<boolean>(`${this.apiPrefix}/Data/Crop`, dto)) as Promise<any>;
  }
}
