import { DistancePipe } from './distance.pipe';

describe('DistancePipe', () => {

  it('create an instance', () => {
    const pipe = new DistancePipe();
    expect(pipe).toBeTruthy();
  });

  it('should transform km', () => {
    const pipe = new DistancePipe();
    const res = pipe.transform(12.1234);
    expect(res).toBe('12.1 km');
  });

  it('shlould transform m', () => {
    const pipe = new DistancePipe();
    const res = pipe.transform(0.12345678);
    expect(res).toBe('123 m');
  })

  it('should ignore crap', () => {
    const pipe = new DistancePipe();
    const res = pipe.transform('crap');
    expect(res).toBe('crap');
  });

});
