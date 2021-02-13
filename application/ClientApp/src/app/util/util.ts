export function getImageFromCategory(sportCat: string): string {
    console.log(sportCat);
    switch (sportCat) {
      case 'RUNNING':
        return '/assets/images/Laufen.png';
      case 'Biking':
        return '/assets/images/Mountainbike.png';
    }
    return '/assets/images/AlpineTrailrun.png';
}

