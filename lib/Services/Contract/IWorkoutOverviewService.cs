using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface IWorkoutOverviewService
{
    Task<PagedResultDto<WorkoutOverviewItemDto>> SearchWorkoutItems(SearchWorkoutsDto search);
}