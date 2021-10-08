import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutOverView2Component } from './workout-over-view2.component';

describe('WorkoutOverView2Component', () => {
  let component: WorkoutOverView2Component;
  let fixture: ComponentFixture<WorkoutOverView2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutOverView2Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkoutOverView2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
