import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Statistics } from '../model/statistics/statistics';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {

  public statistics: Statistics = {
    items: []
  };

  public displayedColumns: string[] = ['sportsCategoryName', 'distanceInKm'];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.load();
  }

  private async load(): Promise<void> {
    this.statistics = await this.dataService.getStatistics();
  }

}
