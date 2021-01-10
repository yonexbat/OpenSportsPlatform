using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class WorkoutService : IWorkoutService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;

        public WorkoutService(OpenSportsPlatformDbContext dbContext,
            ILogger<WorkoutService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<WorkoutDto> GetWorkout(int id)
        {
            var res = await _dbContext
                .Workout
                .Where(x => x.Id == id)
                .Select(x => new WorkoutDto()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Sport = x.SportsCategory.Name,
                }).SingleAsync();

            return res;
        }
    }
}
