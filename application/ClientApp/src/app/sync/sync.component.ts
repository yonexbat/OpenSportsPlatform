import { Component, OnInit } from '@angular/core';
import { ConfirmService } from '../confirm.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-sync',
  templateUrl: './sync.component.html',
  styleUrls: ['./sync.component.scss']
})
export class SyncComponent implements OnInit {

  constructor(private dataService: DataService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
  }

  public async syncPolar(): Promise<void> {
    await this.dataService.syncPolar();
    await this.confirmService.inform('synchronize', 'synchronization is done');
  }

}
