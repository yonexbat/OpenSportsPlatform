export function getImageFromCategory(sportCat?: string): string {
    switch (sportCat) {
      case 'RUNNING':
        return '/assets/images/LaufenSmall.png';
      case 'Biking':
      case 'MOUNTAIN_BIKING':
        return '/assets/images/MountainbikeSmall.png';
      case 'CYCLING_SPORT':
        return '/assets/images/VeloSmall.png';
      case 'WALKING':
      case 'HIKING':
        return '/assets/images/AlpineTrailrunSmall.png';
    }
    return '/assets/images/OtherSmall.png';
}


export function ticksToString(ticks: number): string {
  ticks = ticks / 1000;
  const hh = Math.floor(ticks / 3600);
  const mm = Math.floor((ticks % 3600) / 60);
  return `${pad(hh, 2)}:${pad(mm, 2)}`;
}

function pad(n: number, width: number): string {
  const q = n + '';
  return q.length >= width ? q : new Array(width - q.length + 1).join('0') + q;
}


