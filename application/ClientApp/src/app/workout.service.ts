import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Workout } from './model/workout/workout';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {

  constructor(private dataService: DataService) { }

  public async getWorkout(id: number): Promise<Workout> {
    let workout = this.loadWorkout(id);
    if(!workout) {
      workout = await this.getWorkoutFromServer(id);
      this.storeLocally(workout);
    }
    return workout;
  }

  public clearWorkout(id: number){
    const key = this.getKey(id);
    localStorage.removeItem(key);
  }

  private storeLocally(workout: Workout) {
    const json = JSON.stringify(workout);
    const key = this.getKey(workout.id);
    localStorage.setItem(key, json);
  }

  private loadWorkout(id: number): Workout | undefined {
    const key = this.getKey(id);
    const json= localStorage.getItem(key);
    if(json) {
      return JSON.parse(json);
    }
    return undefined;
  }

  private async getWorkoutFromServer(id: number): Promise<Workout> {
    const workout = await this.dataService.getWorkout(id);
    const samples = await this.dataService.getSamples(id);
    workout.samples = samples;
    this.storeLocally(workout);
    return workout;
  }

  private getKey(id?: number): string {
    return `workout${id}`;
  }

}
