using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Security;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Model.Exceptions;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class CropWorkoutService : ICropWorkoutService
    {

        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ISecurityService _securityService;

        public CropWorkoutService(OpenSportsPlatformDbContext dbContext,
            ILogger<CropWorkoutService> logger,
            ISecurityService securityService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _securityService = securityService;
        }


        public async Task Crop(CropWorkoutDto dto)
        {

            
            long cropFrom = Math.Min(dto.CropFrom, dto.CropTo);
            long cropTo = Math.Max(dto.CropFrom, dto.CropTo);
            int id = dto.Id;

            _logger.LogInformation("Cropping workout with id {0}.", id);

            var securedEntity = await _dbContext.Workout.Where(x => x.Id == id)
                .Select(x => new SecuredEntityDto()
                {
                    OwnerUserId = x.UserProfile.UserId,
                }).SingleOrDefaultAsync();

            if (securedEntity == null)
            {
                throw new EntityNotFoundException(typeof(Workout), id);
            }

            _securityService.CheckAccess(securedEntity);

            var firstTimeStamp = await _dbContext.Sample
                .Where(x => x.Segment.Workout.Id == id)
                .Where(x => x.Timestamp.HasValue)
                .OrderBy(x => x.Id)
                .MinAsync(x => x.Timestamp);

            

            if (firstTimeStamp.HasValue)
            {
                TimeSpan cropFromTimespan = new TimeSpan(cropFrom * TimeSpan.TicksPerMillisecond);
                TimeSpan cropToTimespan = new TimeSpan(cropTo * TimeSpan.TicksPerMillisecond);


                DateTimeOffset keepFrom = firstTimeStamp.Value + cropFromTimespan;
                DateTimeOffset keepTo = firstTimeStamp.Value + cropToTimespan;
            }

        }
    }
}
