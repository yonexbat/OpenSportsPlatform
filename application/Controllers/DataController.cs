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
        [AllowAnonymous]
        [Route("[action]")]
        public Task<IList<WorkoutOverviewItemDto>> SearchWorkoutItems([FromQuery]SearchWorkoutsDto search)
        {
            return _workoutService.SearchWorkoutItems(search);
        }
    }
}
