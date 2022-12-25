import { animate, state, style, transition, trigger } from '@angular/animations';
import { HttpClient, HttpErrorResponse, HttpEventType, HttpRequest } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { of, Subscription } from 'rxjs';
import { catchError, last, map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.scss'],
  animations: [
    trigger('fadeInOut', [
      state('in', style({ opacity: 100 })),
      transition('* => void', [
        animate(300, style({ opacity: 0 }))
      ])
    ])
  ]
})
export class FileUploaderComponent {

  @Input() text = 'Upload';

  @Input() param = 'files';

  @Input() target = '/Data/UploadTcxFiles';

  @Input() accept = 'image/*';

  @Output() uploadComplete = new EventEmitter<string>();

  public files: Array<FileUploadModel> = [];

  constructor(private httpClient: HttpClient) { }

  public onClick(): void {
    const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
    fileUpload.onchange = () => {

      if (!fileUpload.files) {
        return;
      }

      for (let index = 0; index < fileUpload.files.length; index++) {
        const file = fileUpload.files[index];
        this.files.push({
          data: file,
          state: 'in',
          inProgress: false,
          progress: 0,
          canRetry: false,
          canCancel: true,
        });
      }
      this.uploadFiles();
    };
    fileUpload.click();
  }

  private uploadFile(file: FileUploadModel): void {
    const fd = new FormData();
    fd.append(this.param, file.data);

    const req = new HttpRequest('POST', this.target, fd, {
      reportProgress: true
    });

    file.inProgress = true;
    file.sub = this.httpClient.request(req).pipe(
      map(event => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            if (event.total) {
              file.progress = Math.round(event.loaded * 100 / event.total);
            }
            break;
          case HttpEventType.Response:
            return event;
        }
        return event;
      }),
      last(),
      catchError((error: HttpErrorResponse) => {
        file.inProgress = false;
        file.canRetry = true;
        return of(`${file.data.name} upload failed.`);
      })
    ).subscribe(
      (event: any) => {
        if (typeof (event) === 'object') {
          this.removeFileFromArray(file);
          this.uploadComplete.emit(event.body);
        }
      }
    );
  }

  public cancelFile(file: FileUploadModel): void {
    if (!file) {
      return;
    }
    if (file.sub) {
      file.sub.unsubscribe();
    }
    this.removeFileFromArray(file);
  }

  public retryFile(file: FileUploadModel): void {
    this.uploadFile(file);
    file.canRetry = false;
  }

  private uploadFiles(): void {
    const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
    fileUpload.value = '';

    this.files.forEach(file => {
      this.uploadFile(file);
    });
  }

  private removeFileFromArray(file: FileUploadModel): void {
    const index = this.files.indexOf(file);
    if (index > -1) {
      this.files.splice(index, 1);
    }
  }


}

export interface FileUploadModel {
  data: File;
  state: string;
  inProgress: boolean;
  progress: number;
  canRetry: boolean;
  canCancel: boolean;
  sub?: Subscription;
}
