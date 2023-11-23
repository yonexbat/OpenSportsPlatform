using OpenSportsPlatform.Lib.Model.Dtos.Workout;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface ICropWorkoutService
{
    Task Crop(CropWorkoutDto dto);
}