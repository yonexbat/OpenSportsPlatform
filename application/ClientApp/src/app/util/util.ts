export function getImageFromCategory(sportCat?: string): string {
    switch (sportCat) {
      case 'RUNNING':
        return '/assets/images/Laufen.png';
      case 'Biking':
        return '/assets/images/Mountainbike.png';
    }
    return '/assets/images/AlpineTrailrun.png';
}

