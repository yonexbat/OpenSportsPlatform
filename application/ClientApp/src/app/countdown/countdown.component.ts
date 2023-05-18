import { Component, OnInit } from '@angular/core';
import { interval, Subscription } from 'rxjs';


@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss']
})
export class CountdownComponent implements OnInit {




  private timerSubscription?: Subscription;
  public countdown?: Date;

  ngOnInit(): void {
    this.countdown = new Date(0);
  }


  public start(): void {
    const countDownDate = new Date().getTime() + 60000; // 1 minute from now
    this.timerSubscription = interval(100).subscribe(() => {
      const now = new Date().getTime();
      const distance = countDownDate - now;

      if (distance < 0) {
        this.timerSubscription?.unsubscribe();
        this.countdown = new Date(0);
      } else {
        this.countdown = new Date(distance);
      }
    });
  }

  public reset(): void {
    this.timerSubscription?.unsubscribe();
    this.countdown = new Date(0);
  }

}
