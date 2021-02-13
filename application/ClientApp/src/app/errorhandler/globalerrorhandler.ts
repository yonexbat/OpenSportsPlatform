import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { ConfirmService } from '../confirm.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor(private zone: NgZone, private confirmService: ConfirmService) { }

    handleError(error: Error): void {

        const errormessage: any = error.message  || error.toString();
        this.zone.run(() => this.confirmService.inform('Error', errormessage));

        console.error('Error from global error handler', error);
    }
}
