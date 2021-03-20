import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Statistics } from '../model/statistics/statistics';
import { getImageFromCategory } from '../util/util';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {

  public statistics: Statistics = {
    monthItems: [],
    yearItems: [],
  };

  public displayedColumns: string[] = ['sportsCategoryName', 'distanceInKm'];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.load();
  }

  private async load(): Promise<void> {
    this.statistics = await this.dataService.getStatistics();
  }

  public getImage(sportCat: string): string {
    return getImageFromCategory(sportCat);
  }

}
