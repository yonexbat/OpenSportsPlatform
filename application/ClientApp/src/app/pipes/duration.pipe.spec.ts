import { DurationPipe } from './duration.pipe';

describe('DurationPipe', () => {
  it('create an instance', () => {
    const pipe = new DurationPipe();
    expect(pipe).toBeTruthy();
  });

  it('should convert hours', () => {
    const pipe = new DurationPipe();
    const fourHours = 4 * 60 * 60;
    const fiveMinutes = 5 * 60;
    const thirtySec = 30;
    const total = fourHours + fiveMinutes + thirtySec;
    const res = pipe.transform(total);
    expect(res).toBe('4h 5m 30s');
  });

  it('should convert minutes', () => {
    const pipe = new DurationPipe();
    const fiveMinutes = 5 * 60;
    const thirtySec = 30;
    const total = fiveMinutes + thirtySec;
    const res = pipe.transform(total);
    expect(res).toBe('5m 30s');
  });

  it('should convert seconds', () => {
    const pipe = new DurationPipe();
    const thirtySec = 30;
    const res = pipe.transform(thirtySec);
    expect(res).toBe('30s');
  });

});
