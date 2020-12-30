import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutOverViewComponent } from './workout-over-view.component';

describe('WorkoutOverViewComponent', () => {
  let component: WorkoutOverViewComponent;
  let fixture: ComponentFixture<WorkoutOverViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutOverViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkoutOverViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
