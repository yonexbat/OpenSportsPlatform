using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IWorkoutService
    {
        Task<WorkoutDto> GetWorkout(int id);
    }
}
