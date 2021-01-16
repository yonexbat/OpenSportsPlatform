import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadWorkoutComponent } from './upload-workout.component';

describe('UploadWorkoutComponent', () => {
  let component: UploadWorkoutComponent;
  let fixture: ComponentFixture<UploadWorkoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadWorkoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadWorkoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
