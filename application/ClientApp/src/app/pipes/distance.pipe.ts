import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'distance'
})
export class DistancePipe implements PipeTransform {

  transform(value: unknown): unknown {
    if (this.isNumber(value)) {
      const distInKm = value as number;
      if (distInKm > 1) {
        const distInKmRounded = Math.round(distInKm * 10) / 10;
        return `${distInKmRounded}km`;
      } else {
        const meters = Math.round(distInKm * 1000);
        return `${meters}m`;
      }
    }
    return value;
  }

  private isNumber(value: unknown): boolean {
    return Number.isFinite(value);
  }
}
