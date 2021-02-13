import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ConfirmComponent } from './confirm/confirm.component';
import { ConfirmData } from './model/confirm/confirmdata';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  constructor(public dialog: MatDialog) { }

  public confirm(title: string, text: string): Observable<boolean> {
    return this.shwoDialog(title, text, true);
  }

  public inform(title: string, text: string): Observable<boolean> {
    return this.shwoDialog(title, text, false);
  }

  private shwoDialog(title: string, text: string, showCancel: boolean): Observable<boolean> {
    const data: ConfirmData = {
      text,
      title,
      showCancel,
    };
    return this.dialog.open(ConfirmComponent, {
      height: '400px',
      width: '600px',
      data,
    }).afterClosed().pipe(map(x => {
      if (x === 'Ok') {
        return true;
      }
      return false;
    }));
  }


}
