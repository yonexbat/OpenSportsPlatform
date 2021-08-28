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

