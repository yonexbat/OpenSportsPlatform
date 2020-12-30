using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IWorkoutOverviewService
    {
        Task<IList<WorkoutOverviewItemDto>> SearchWorkoutItems(SearchWorkoutsDto search);
    }
}
