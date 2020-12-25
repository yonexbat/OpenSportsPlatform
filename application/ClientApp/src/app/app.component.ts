import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { PromiseType } from 'protractor/built/plugins';
import { AuthenticationService } from './authentication.service';
import { DataService } from './data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public title = 'Open Sports Platform';

  constructor() {
  }

  ngOnInit(): void {

  }
}
