using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface ICropWorkoutService
    {
        Task Crop(CropWorkoutDto dto);
    }
}
