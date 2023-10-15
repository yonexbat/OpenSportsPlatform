using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSportsPlatform.Lib.Model.Dtos.Common;
using OpenSportsPlatform.Lib.Model.Dtos.Statistics;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;


namespace OpenSportsPlatform.Application.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IWorkoutOverviewService _workoutOverviewService;
        private readonly IWorkoutService _workoutService;
        private readonly ITcxFileImporterService _tcxFileImporterService;
        private readonly IStatisticsService _statisticsService;
        private readonly ISyncPolarService _syncPolarService;
        private readonly ICropWorkoutService _cropWorkoutService;
        private readonly ISamplesService _samplesService;

        public DataController(IWorkoutOverviewService workoutOverviewService,
            IWorkoutService workoutService,
            ITcxFileImporterService tcxFileImporterService,
            IStatisticsService statisticsService,
            ISyncPolarService syncPolarService,
            ICropWorkoutService cropWorkoutService,
            ISamplesService samplesService)
        {
            _workoutOverviewService = workoutOverviewService;
            _workoutService = workoutService;
            _tcxFileImporterService = tcxFileImporterService;
            _statisticsService = statisticsService;
            _syncPolarService = syncPolarService;
            _cropWorkoutService = cropWorkoutService;
            _samplesService = samplesService;
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
        
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<SampleDto>>> GetSamples([FromRoute] int id)
        {
            var samplesEnumerable = await _samplesService.GetSamples(id);
            return Ok(samplesEnumerable);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<EditWorkoutDto> GetEditWorkout(int id)
        {
            return await _workoutService.GetEditWorkout(id);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task DeleteWorkout(int id)
        {
            await _workoutService.DeleteWorkout(id);           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UploadTcxFiles(List<IFormFile> files)
        {
            foreach(IFormFile file in files)
            {
                var stream = file.OpenReadStream();
                await _tcxFileImporterService.ImportWorkout(stream);   
            }

            return Ok(new { count = files.Count});
        }

        [HttpPost]
        [Route("[action]")]
        public async Task SaveWorkout([FromBody] SaveWorkoutDto dto)
        {
            await _workoutService.SaveWorkout(dto);           
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<StatisticsDto> GetStatistics([FromQuery] GetStatisticsDto dto)
        {
            return await _statisticsService.GetStatistics(dto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task SyncPolar()
        {
            await _syncPolarService.SyncPolar();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Crop([FromBody] CropWorkoutDto dto)
        {
            await _cropWorkoutService.Crop(dto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IList<SelectItemDto>> AddTag([FromBody] AddTagDto dto)
        {
            return await _workoutService.AddTag(dto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IList<SelectItemDto>> RemoveTag([FromBody] RemoveTagDto dto)
        {
            return await _workoutService.RemoveTag(dto);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IList<SelectItemDto>> SearchTags([FromQuery] string name)
        {
            return await _workoutService.SearchTags(name);
        }
    }
}
