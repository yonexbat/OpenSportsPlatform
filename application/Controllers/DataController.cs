using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
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
        private readonly IWorkoutOverviewService _workoutOverviewService;
        private readonly IWorkoutService _workoutService;

        public DataController(IWorkoutOverviewService workoutOverviewService,
            IWorkoutService workoutService)
        {
            _workoutOverviewService = workoutOverviewService;
            _workoutService = workoutService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<PagedResultDto<WorkoutOverviewItemDto>> SearchWorkoutItems([FromQuery] SearchWorkoutsDto search)
        {
            return await _workoutOverviewService.SearchWorkoutItems(search);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<WorkoutDto> GetWorkout(int id)
        {
            return await _workoutService.GetWorkout(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadTcxFiles(List<IFormFile> files)
        {

            return Ok(new { count = files.Count, size = 4 });
        }
    }
}
