import { Component, OnInit } from '@angular/core';
import { ConfirmService } from '../confirm.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-sync',
  templateUrl: './sync.component.html',
  styleUrls: ['./sync.component.scss']
})
export class SyncComponent implements OnInit {

  public showSpinner = false;

  constructor(private dataService: DataService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
  }

  public async syncPolar(): Promise<void> {
    try {
      this.showSpinner = true;
      await this.dataService.syncPolar();
      await this.confirmService.inform('synchronize', 'synchronization is done');
    } finally {
      this.showSpinner = false;
    }
  }

}
