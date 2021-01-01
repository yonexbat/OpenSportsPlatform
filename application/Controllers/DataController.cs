using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Application.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IWorkoutOverviewService _workoutService;
        public DataController(IWorkoutOverviewService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<PagedResultDto<WorkoutOverviewItemDto>> SearchWorkoutItems([FromQuery]SearchWorkoutsDto search)
        {
            return await _workoutService.SearchWorkoutItems(search);
        }
    }
}
