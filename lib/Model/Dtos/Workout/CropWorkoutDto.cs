using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Model.Dtos.Workout
{
    public class CropWorkoutDto
    {
        public int Id { get; set; }

        public long CropFrom { get; set; }

        public long CropTo { get; set; }
    }
}
