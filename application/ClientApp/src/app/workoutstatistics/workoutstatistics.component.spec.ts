import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutstatisticsComponent } from './workoutstatistics.component';

describe('WorkoutstatisticsComponent', () => {
  let component: WorkoutstatisticsComponent;
  let fixture: ComponentFixture<WorkoutstatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutstatisticsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkoutstatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
