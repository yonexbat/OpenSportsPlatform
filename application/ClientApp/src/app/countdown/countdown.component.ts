import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, interval, merge, Observable, Subject } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss']
})
export class CountdownComponent implements OnInit {

  public countDownText$: Observable<string>;

  private numSeconds = 60;

  private doCountDown = false;

  private zero$ = new BehaviorSubject<number>(0);

  static calculateText(numSeconds: number): string {
    return `00:${numSeconds}`;
  }

  constructor() {
    this.countDownText$ =  merge(interval(1000), this.zero$)
      .pipe(map(() => this.countDown()));
  }

  ngOnInit(): void {
  }

  public countDown(): string {
    if (this.doCountDown && this.numSeconds > 0) {
      this.numSeconds -= 1;
    }
    return CountdownComponent.calculateText(this.numSeconds);
  }

  public start(): void {
    this.doCountDown = true;
  }

  public reset(): void {
    this.doCountDown = false;
    this.numSeconds = 60;
    this.zero$.next(0);
  }

}
