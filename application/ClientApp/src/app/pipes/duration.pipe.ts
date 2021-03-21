import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'duration'
})
export class DurationPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    if (this.isNumber(value)) {
      const durationInSec = value as number;
      const hours = Math.floor(durationInSec / (60.0 * 60.0));
      const residueFromHours = durationInSec - (hours * 60.0 * 60.0);
      const minutes = Math.floor(residueFromHours / 60.0);
      const seconds = Math.round(residueFromHours - (minutes * 60));
      if (hours > 0) {
        return `${hours}h ${minutes}m ${seconds}s`;
      }
      else {
        if(minutes > 0) {
          return `${minutes}m ${seconds}s`;
        }
        return `${seconds}s`;
      }
    }
    return value;
  }

  private isNumber(value: unknown): boolean {
    return Number.isFinite(value);
  }

}
