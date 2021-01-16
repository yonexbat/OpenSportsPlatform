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
        private readonly ITcxFileImporterService _tcxFileImporterService;

        public DataController(IWorkoutOverviewService workoutOverviewService,
            IWorkoutService workoutService,
            ITcxFileImporterService tcxFileImporterService)
        {
            _workoutOverviewService = workoutOverviewService;
            _workoutService = workoutService;
            _tcxFileImporterService = tcxFileImporterService;
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
        [Route("[action]")]
        public async Task<IActionResult> UploadTcxFiles(List<IFormFile> files)
        {
            foreach(IFormFile file in files)
            {
                try
                {
                    var stream = file.OpenReadStream();
                    await _tcxFileImporterService.ImoportWorkout(stream);
                } 
                catch(Exception ex)
                {
                    throw;
                }
                
            }

            return Ok(new { count = files.Count, size = 4 });
        }
    }
}
