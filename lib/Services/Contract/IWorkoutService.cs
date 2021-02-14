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

        Task<bool> DeleteWorkout(int id);

        Task<bool> SaveWorkout(SaveWorkoutDto dto);

        Task<EditWorkoutDto> GetEditWorkout(int id);
    }
}
