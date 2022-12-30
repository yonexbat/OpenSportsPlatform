﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model;
using OpenSportsPlatform.Lib.Model.Dtos.Common;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Model.Exceptions;
using OpenSportsPlatform.Lib.Services.Contract;


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

        public async Task AddTag(AddTagDto dto)
        {
            if (dto.Id == null)
            {
                throw new ArgumentNullException("Id must not be null");
            }

            if (dto.Name == null)
            {
                throw new ArgumentNullException("Name must not be null");
            }

            Workout? workout = await GetWorkoutSecured(dto.Id ?? 0);
            Tag? tag = await _dbContext.Tag
                .Where(x => x.Name == dto.Name)
                .SingleOrDefaultAsync();

            if (tag == null)
            {
                tag = new Tag { Name = dto.Name };
                await _dbContext.AddAsync(tag);
            }

            TagWorkout? tagWorkout = await _dbContext.TagWorkout
                .Where(x => x.WorkoutId == dto.Id && x.TagId == tag.Id)
                .SingleOrDefaultAsync();

            if (tagWorkout == null)
            {
                tagWorkout = new TagWorkout { 
                    Tag = tag,
                    Workout = workout,
                };
                await _dbContext.AddAsync(tagWorkout);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteWorkout(int id)
        {
            _logger.LogInformation("Deleting workout with id {0}", id);
            Workout workout = await _dbContext.Workout.Where(x => x.Id == id)
                .Include(x => x.UserProfile!)
                .Include(x => x.Segments!)
                .ThenInclude(x => x.Samples)
                .SingleAsync();

            _securityService.CheckAccess(workout);

            _dbContext.Remove(workout);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EditWorkoutDto> GetEditWorkout(int id)
        {
            _logger.LogDebug("GetEditWorkout. Id: {0}", id);
            EditWorkoutDto result = await _dbContext.Workout
                .Where(x => x.Id == id)
                .Select(x => new EditWorkoutDto()
                {
                    Id = x.Id,
                    SportsCategoryId = x.SportsCategoryId,
                    Notes = x.Notes,
                }).SingleAsync();

            result.FirstSampleTimestamp = await _dbContext.Sample
                .Where(x => x.Segment!.Workout!.Id == id)
                .Where(x => x.Timestamp.HasValue)
                .OrderBy(x => x.Id)
                .MinAsync(x => x.Timestamp);
                

            result.LastSampleTimestamp = await _dbContext.Sample
                .Where(x => x.Segment!.Workout!.Id == id)
                .Where(x => x.Timestamp.HasValue)
                .OrderBy(x => x.Id)
                .MaxAsync(x => x.Timestamp);

            result.SportsCategories = await _dbContext.SportsCategory.Select(
                x => new SelectItemDto()
                {
                    Id = x.Id,
                    Name = x.Name!,
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            result.Ticks = (result.LastSampleTimestamp - result.FirstSampleTimestamp)?.TotalMilliseconds;

            return result;
        }

        public async Task<WorkoutDto> GetWorkout(int id)
        {
            Workout workout = await GetWorkoutSecured(id);

            WorkoutDto res = new WorkoutDto()
            {
                Id = workout.Id,
                StartTime = workout.StartTime,
                EndTime = workout.EndTime,
                Sport = workout.SportsCategory!.Name,
                DistanceInKm = workout.DistanceInKm,
                AscendInMeters = workout.AscendInMeters,
                DescendInMeters = workout.DescendInMeters,
                CaloriesInKCal = workout.CaloriesInKCal,
                HeartRateAvgBpm = workout.HeartRateAvgBpm,
                HeartRateMaxBpm = workout.HeartRateMaxBpm,
                DurationInSec = workout.DurationInSec,
            };

            //Samples
            res.Samples = await _dbContext
                .Sample
                .Where(x => x.Segment!.Workout!.Id == id)
                .Where(x => x.Latitude.HasValue && x.Longitude.HasValue)
                .OrderBy(x => x.Timestamp)
                .Select(x => new SampleDto()
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    AltitudeInMeters = x.AltitudeInMeters,
                    HeartRateBpm = x.HeartRateBpm,
                    Timestamp = x.Timestamp,
                }).ToListAsync();

            return res;
        }

        public async Task RemoveTag(RemoveTagDto dto)
        {
            if (dto.Id == null)
            {
                throw new ArgumentNullException("Id must not be null");
            }

            if (dto.Name == null)
            {
                throw new ArgumentNullException("Name must not be null");
            }

            await GetWorkoutSecured(dto.Id ?? 0);
            TagWorkout? tagWorkout = await _dbContext.TagWorkout
                .Where(x => x.Workout.Id == dto.Id)
                .Where(x => x.Tag.Name == dto.Name)
                .SingleOrDefaultAsync();

            if (tagWorkout != null) {
                _dbContext.Remove(tagWorkout);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveWorkout(SaveWorkoutDto dto)
        {
            Workout workout = await GetWorkoutSecured(dto.Id ?? 0);

            workout.SportsCategoryId = dto.SportsCategoryId ?? throw new ArgumentNullException(nameof(SaveWorkoutDto.SportsCategoryId));
            workout.Notes = dto.Notes;

            await _dbContext.SaveChangesAsync();
        }

        private async Task<Workout> GetWorkoutSecured(int workoutId)
        {
            var workout = await _dbContext.Workout
                .Where(x => x.Id == workoutId)
                .Include(x => x.UserProfile)
                .Include(x => x.SportsCategory)
                .SingleAsync();

            if (workout == null)
            {
                throw new EntityNotFoundException(typeof(Workout), workoutId);
            }

            _securityService.CheckAccess(workout);

            return workout;
        }

        public async Task<IList<SelectItemDto>> SerachTags(string name)
        {
            return await _dbContext.Tag
                .Where(x => x.Name.Contains(name))
                .Take(100)
                .OrderBy(x => x.Name)
                .Select(x => new SelectItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }
    }
}
