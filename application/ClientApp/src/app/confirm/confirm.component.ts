import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ConfirmData } from '../model/confirm/confirmdata';


@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent {

  constructor(
    public dialogRef: MatDialogRef<ConfirmComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmData) { }

  public okClicked(): void {
    this.dialogRef.close('Ok');
  }

  public cancelClicked(): void {
    this.dialogRef.close('Cancel');
  }
}
