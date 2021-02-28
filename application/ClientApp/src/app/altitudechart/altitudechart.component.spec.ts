import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AltitudechartComponent } from './altitudechart.component';

describe('AltitudechartComponent', () => {
  let component: AltitudechartComponent;
  let fixture: ComponentFixture<AltitudechartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AltitudechartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AltitudechartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
