using OpenSportsPlatform.Lib.Model.Dtos.Common;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface IWorkoutService
{
    Task<WorkoutDto> GetWorkout(int id);

    Task DeleteWorkout(int id);

    Task SaveWorkout(SaveWorkoutDto dto);

    Task<EditWorkoutDto> GetEditWorkout(int id);

    Task<IList<SelectItemDto>> AddTag(AddTagDto dto);

    Task<IList<SelectItemDto>> RemoveTag(RemoveTagDto dto);

    Task<IList<SelectItemDto>> SearchTags(string name);
}