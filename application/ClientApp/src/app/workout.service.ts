import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Workout } from './model/workout/workout';

const DATABASE = "OpenSportsPlatform";
const WORKOUT_TABLE = "workouts";

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {
 

  constructor(private dataService: DataService) { }

  public async getWorkout(id: number): Promise<Workout> {
    let workout = await this.loadWorkout(id);
    if (!workout) {
      workout = await this.getWorkoutFromServer(id);
      await this.storeLocally(workout);
    }
    return workout;
  }

  public clearWorkout(id: number) {
    const key = this.getKey(id);
    localStorage.removeItem(key);
  }

  private async storeLocally(workout: Workout): Promise<void> {
    const result = new Promise<void>((resolve, reject) => {
      const request = this.startIndexedDb();

      request.onsuccess = () => {
        const db = request.result;
        const tx = db.transaction(WORKOUT_TABLE, "readwrite");
        const store = tx.objectStore(WORKOUT_TABLE);
        const storeRequest = store.put(workout);
        storeRequest.onsuccess = () => {
          resolve();
        };
        storeRequest.onerror = () => {
          reject();
        }
        tx.oncomplete = () => {
          db.close();          
        }        
      }
      request.onerror = () => {
        reject();
      }
    });
    return result;
  }

  private startIndexedDb(): IDBOpenDBRequest {
    const request = window.indexedDB.open(DATABASE, 1);

    request.onupgradeneeded = () => {
      const db = request.result;
      db.createObjectStore(WORKOUT_TABLE, { keyPath: "id" });
    }
    return request;
  }


  private loadWorkout(id: number): Promise<Workout | undefined> {
    const result = new Promise<Workout | undefined>((resolve, reject) => {
      const request = this.startIndexedDb();

      request.onsuccess = () => {
        const db = request.result;
        const tx = db.transaction(WORKOUT_TABLE, "readonly");
        const store = tx.objectStore(WORKOUT_TABLE);
        const workoutDbRequest = store.get(id);
        workoutDbRequest.onerror = () => {
          reject();
        };
        workoutDbRequest.onsuccess = () => {
          resolve(workoutDbRequest.result);
        }      
        tx.oncomplete = () => {
          db.close();
        }
      }
      request.onerror = () => {
        reject();
      }
    });
    return result;
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
