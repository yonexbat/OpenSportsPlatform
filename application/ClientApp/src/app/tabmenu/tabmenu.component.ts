import { Component, OnInit } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ActivatedRoute, Params, Router, UrlSegment } from '@angular/router';
import { WorkoutMenuItem } from '../model/workout/workoutMenuItem';

@Component({
  selector: 'app-tabmenu',
  templateUrl: './tabmenu.component.html',
  styleUrls: ['./tabmenu.component.scss']
})
export class TabmenuComponent implements OnInit {

  links: WorkoutMenuItem[] = [
    {
      name: 'Map',
      routerLink: 'workout'
    },
    {
      name: 'Statistics',
      routerLink: 'workoutstatistics'
    },
    {
      name: 'Edit Workout',
      routerLink: 'editworkout'
    }
  ];

  public id?: number;
  public path?: string;

  background: ThemePalette = undefined;

  constructor(private route: ActivatedRoute,  private router: Router) {
    this.route.params.subscribe(x => this.handleRouteParamChanged(x));
    this.route.url.subscribe(x => this.handleRouteChanged(x));
  }

  itemClicked(link: WorkoutMenuItem): void {
    console.log('item clicked');
    this.router.navigate([link.routerLink, this.id]);
  }

  ngOnInit(): void {
  }

  handleRouteParamChanged(params: Params): void {
    this.id = params.id;
  }

  handleRouteChanged(urlSeg: UrlSegment[]): void {
    this.path = urlSeg[0].path;
  }
}
