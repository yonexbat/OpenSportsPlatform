using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class WorkoutService : IWorkoutService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ISecurityService _securityService;

        public WorkoutService(OpenSportsPlatformDbContext dbContext,
            ILogger<WorkoutService> logger,
            ISecurityService securityService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _securityService = securityService;
        }

        public async Task<bool> DeleteWorkout(int id)
        {
            _logger.LogInformation("Deleting workout with id {0}", id);
            Workout wo = await _dbContext.Workout.Where(x => x.Id == id)
                .Include(x => x.UserProfile)
                .SingleAsync();

            string userId = wo.UserProfile.UserId;
            string currentPrincipal = _securityService.GetCurrentUserid();

            if(userId != currentPrincipal)
            {
                throw new SecurityException($"User {currentPrincipal} not allowed to delete workout from {userId}");
            }

            _dbContext.Remove(wo);
            await _dbContext.SaveChangesAsync();
            return true;
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

            //Samples
            res.Samples = await _dbContext
                .Sample
                .Where(x => x.Segment.Workout.Id == id)
                .Where(x => x.Latitude.HasValue && x.Longitude.HasValue)
                .OrderBy(x => x.Timestamp)
                .Select(x => new SampleDto()
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                }).ToListAsync();

            return res;
        }
    }
}
