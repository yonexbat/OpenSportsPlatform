import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { PagedResult } from './model/common/pagedresult';
import { SelectItem } from './model/common/selectitem';
import { AddTag } from './model/editworkout/addTag';
import { CropWorkout } from './model/editworkout/cropWorkout';
import { EditWorkout } from './model/editworkout/editWorkout';
import { RemoveTag } from './model/editworkout/removeTag';
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

  public saveWorkout(dto: SaveWorkout): Promise<void> {
    return firstValueFrom(this.http.post<void>(`${this.apiPrefix}/Data/SaveWorkout`, dto)) as Promise<void> ;
  }

  public deleteWorkout(id: number): Promise<void> {
    return firstValueFrom(this.http.delete<void>(`${this.apiPrefix}/Data/DeleteWorkout/${id}`)) as Promise<void>;
  }

  public getStatistics(): Promise<Statistics> {
    return firstValueFrom(this.http.get<Statistics>(`${this.apiPrefix}/Data/GetStatistics`)) as Promise<Statistics>;
  }

  public syncPolar(): Promise<void> {
    return firstValueFrom(this.http.post<void>(`${this.apiPrefix}/Data/SyncPolar`, {})) as Promise<void>;
  }

  public crop(dto: CropWorkout): Promise<void> {
    return firstValueFrom(this.http.post<void>(`${this.apiPrefix}/Data/Crop`, dto)) as Promise<void>;
  }

  public addTag(dto: AddTag): Promise<SelectItem[]> {
    return firstValueFrom(this.http.post<SelectItem[]>(`${this.apiPrefix}/Data/AddTag`, dto)) as Promise<SelectItem[]>;
  }

  public removeTag(dto: RemoveTag): Promise<SelectItem[]> {
    return firstValueFrom(this.http.post<SelectItem[]>(`${this.apiPrefix}/Data/RemoveTag`, dto)) as Promise<SelectItem[]>;
  }

  public searchTags(name: string): Promise<SelectItem[]> {
    name = encodeURIComponent(name);
    return firstValueFrom(this.http.get<SelectItem[]>(`${this.apiPrefix}/Data/RemoveTag?name=${name}`)) as Promise<SelectItem[]>;
  }
}
