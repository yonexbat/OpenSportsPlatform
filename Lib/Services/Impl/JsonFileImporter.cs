using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class JsonFileImporterService : IJsonFileImporterService
    {
        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public JsonFileImporterService(
            ILogger<JsonFileImporterService> logger, 
            OpenSportsPlatformDbContext dbContext,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task ImportFiles()
        {
            _logger.LogInformation("Importing files");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM dbo.OSPWorkout");
            List<string> list = GetFileList();
            int index = 0;
            foreach(string file in list)
            {
                await ReadFile(file);
                if(index % 50 == 0)
                {
                    await _dbContext.SaveChangesAsync();
                    _dbContext.ChangeTracker.Clear();
                }
                index++;
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task ReadFile(string fileNameAndPath)
        {
            var stream = File.OpenRead(fileNameAndPath);
            JsonDocument document = await JsonDocument.ParseAsync(stream);
            JsonElement rooteElem = document.RootElement;
            Workout wo = new Workout();

            foreach (JsonElement elem in rooteElem.EnumerateArray())
            {
                foreach(JsonProperty property in elem.EnumerateObject())
                {
                    _logger.LogTrace($"{property.Name}, {property.Value}");
                    if(property.Name == "sport")
                    {
                        wo.SportsCategory = await GetSportCat(property.Value.GetString());
                    }
                    if(property.Name == "start_time")
                    {
                        wo.StartTime = GetDateTime(property.Value.GetString());
                    }
                    if(property.Name == "end_time")
                    {
                        wo.EndTime = GetDateTime(property.Value.GetString());
                    }
                    if (property.Name == "duration_s")
                    {
                        wo.DurationInSec = GetFloat(property);
                    }
                    if (property.Name == "distance_km")
                    {
                        wo.DistanceInKm = GetFloat(property);
                    }
                    if (property.Name == "calories_kcal")
                    {
                        wo.CaloriesInKCal = GetFloat(property);
                    }
                    if (property.Name == "altitude_min_m")
                    {
                        wo.AltitudeMinInMeters = GetFloat(property);
                    }
                    if (property.Name == "altitude_max_m")
                    {
                        wo.AltitudeMaxInMeters = GetFloat(property);
                    }
                    if (property.Name == "heart_rate_avg_bpm")
                    {
                        wo.HeartRateAvgBpm = GetFloat(property);
                    }
                    if (property.Name == "heart_rate_max_bpm")
                    {
                        wo.HeartRateMaxBpm = GetFloat(property);
                    }                    
                    if (property.Name == "cadence_avg_rpm")
                    {
                        wo.CadenceAvgRpm = GetFloat(property);
                    }
                    if (property.Name == "cadence_max_rpm")
                    {
                        wo.CadenceMaxRpm = GetFloat(property);
                    }
                    if (property.Name == "speed_avg_kmh")
                    {
                        wo.SpeedAvgKmh = GetFloat(property);
                    }
                    if (property.Name == "speed_max_kmh")
                    {
                        wo.SpeedMaxKmh = GetFloat(property);
                    }
                    if (property.Name == "ascend_m")
                    {
                        wo.AscendInMeters = GetFloat(property);
                    }
                    if (property.Name == "descend_m")
                    {
                        wo.DescendInMeters = GetFloat(property);
                    }

                }
            }
            
           
            await _dbContext.AddAsync(wo);
        }

        private DateTime? GetDateTime(string value)
        {
            return DateTime.Parse(value);
        }

        private float? GetFloat(JsonProperty property)
        {
            double x = 0;
            if(property.Value.TryGetDouble(out x))
            {
                return (float) x;
            }
            return null;
        }

        private async Task<SportsCategory> GetSportCat(string name)
        {
            SportsCategory sc =  await _dbContext.SportsCategory.Where(x => x.Name == name)
                .FirstOrDefaultAsync();
            if(sc == null)
            {
                sc = new SportsCategory
                {
                    Name = name,
                };
                await _dbContext.AddAsync(sc);
                await _dbContext.SaveChangesAsync();
            }
            return sc;
        }

        private List<string> GetFileList()
        {
            string directory = _configuration.GetValue<string>("WorkoutFilesDirectory");
            _logger.LogInformation("Workoutdirectory: {0}", directory);

            string[] fileEntries = Directory.GetFiles(directory);
            return fileEntries.Where(x => x.EndsWith(".json")).ToList();
        }
    }
}
