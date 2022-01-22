import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ConfirmService } from '../confirm.service';
import { DataService } from '../data.service';
import { SelectItem } from '../model/common/selectitem';
import { CropWorkout } from '../model/editworkout/cropWorkout';
import { SaveWorkout } from '../model/editworkout/saveWorkout';
import { ticksToString } from '../util/util';

@Component({
  selector: 'app-editworkout',
  templateUrl: './editworkout.component.html',
  styleUrls: ['./editworkout.component.scss']
})
export class EditworkoutComponent implements OnInit {

  public sports?: SelectItem[] = [];

  private _sliderValCropFrom: number | null = 0;
  public get sliderValCropFrom() : number | null {
    return this._sliderValCropFrom;
  }
  public set sliderValCropFrom(val: number | null) {
    this._sliderValCropFrom = val;
    if(val && this.ticks) {
      const currentPoint = (val / 10000) * this.ticks;
      this.sliderValCropFromText = ticksToString(currentPoint);
    }
  }

  private _sliderValCropTo: number | null = 10000;
  public get sliderValCropTo() : number | null {
    return this._sliderValCropTo;
  }
  public set sliderValCropTo(val: number | null) {
    this._sliderValCropTo = val;
    if(val && this.ticks) {
      const currentPoint = (val / 10000) * this.ticks;
      this.sliderValCropToText = ticksToString(currentPoint);
    }
  }

  public sliderValCropFromText: string = '00:00';
  public sliderValCropToText: string = '00:00';

  public formGroup: FormGroup = this.fb.group({
    id: [0, Validators.required],
    sportsCategoryId: [0, Validators.required],
    notes: [''],
  });

  private ticks: number = 0;

  constructor(private fb: FormBuilder,
              private dataService: DataService,
              private route: ActivatedRoute,
              private router: Router,
              private confirmService: ConfirmService,
              private snackBar: MatSnackBar) {
      this.route.params.subscribe(x => this.handleRouteParamChanged(x));
  }

  ngOnInit(): void {
  }

  handleRouteParamChanged(params: Params): void {
    const id = params['id'];
    this.loadData(id);
  }

  async loadData(id: number): Promise<void> {
    const workout = await this.dataService.getEditWorkout(id);
    this.sports = workout?.sportsCategories;
    this.formGroup.patchValue(workout);
    this.ticks = workout.ticks;
    this.sliderValCropToText = ticksToString(this.ticks);
  }

  public saveClick(): void {
    this.save();
  }

  async save(): Promise<void> {
    const saveWorkout = this.formGroup.value;
    await this.dataService.saveWorkout(saveWorkout);
    this.snackBar.open('Save successful', 'close', {
      duration: 3000
    }).onAction().subscribe((action: any) => {

    });
  }

  public deleteClick(): void {
    this.confirmService.confirm('Delete workout', 'Do you really want to delete this workout?')
      .subscribe(x => {
        if (x) {
          this.deleteWorkout();
        }
      });
  }

  private async deleteWorkout(): Promise<void> {
    const id = this.formGroup.get('id')?.value;
    if (id > 0) {
      await this.dataService.deleteWorkout(id);
      this.router.navigate(['workouts']);
    }
  }

  public cropClick() {
    this.crop();
  }

  private async crop() {
    const crop: CropWorkout = {
      id: this.formGroup.get('id')?.value,
      cropFrom: this.sliderValCropFrom ?? 0,
      cropTo: this.sliderValCropTo ?? 0,
    };

    await this.dataService.crop(crop);
    await this.loadData(crop.id);
  }
}
