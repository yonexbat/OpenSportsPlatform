import { Component, OnInit } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Params, Router, UrlSegment } from '@angular/router';
import { filter } from 'rxjs/operators';
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
      routerLink: 'map'
    },
    {
      name: 'Statistics',
      routerLink: 'stats'
    },
    {
      name: 'Edit Workout',
      routerLink: 'editworkout'
    }
  ];

  public id?: string;
  public path?: string;

  background: ThemePalette = undefined;

  constructor(private route: ActivatedRoute,  private router: Router) {
    this.router.events
    .pipe(filter(e => e instanceof NavigationEnd)).subscribe(() => this.handleRouteChanged());
  }

  itemClicked(link: WorkoutMenuItem): void {
    this.router.navigate(['.', link.routerLink, this.id], {relativeTo: this.route});
  }

  ngOnInit(): void {
  }


  handleRouteChanged(): void {
    const firstChild = this.route.snapshot.firstChild;
    this.path = firstChild?.url[0]?.path;
    this.id = firstChild?.url[1]?.path;
  }
}
