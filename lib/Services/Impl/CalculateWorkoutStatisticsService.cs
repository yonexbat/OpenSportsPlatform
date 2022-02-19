using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class CalculateWorkoutStatisticsService : ICalculateWorkoutStatisticsService
    {
        private readonly OpenSportsPlatformDbContext _dbContex;
        private readonly ILogger _logger;

        public CalculateWorkoutStatisticsService(
            OpenSportsPlatformDbContext dbContex,
            ILogger<CalculateWorkoutStatisticsService> logger
            )
        {
            _logger = logger;
            _dbContex = dbContex;
        }

        public async Task Calculate(int workoutid)
        {
            _logger.LogInformation("Calculation workout stats");
            var samples = _dbContex.Sample
                .Where(x => x.Segment.Workout.Id == workoutid);

            foreach (var sample in samples)
            {

            }
        }

        private double Dist(Sample sample1, Sample sample2)
        {
            if( !sample1.Latitude.HasValue ||
                !sample1.Longitude.HasValue ||
                !sample2.Latitude.HasValue ||
                !sample2.Longitude.HasValue)
            {
                return 0;
            }

            double dLat = Deg2Rad(sample2.Latitude.Value - sample1.Latitude.Value);
            double dLon = Deg2Rad(sample2.Longitude.Value - sample1.Longitude.Value);
            double r = Math.Sin(dLat / 2) *
              Math.Sin(dLat / 2) +
              Math.Cos(this.Deg2Rad(sample1.Latitude.Value)) *
              Math.Cos(this.Deg2Rad(sample2.Latitude.Value)) *
              Math.Sin(dLon / 2) *
              Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(r), Math.Sqrt(1 - r));
            double d = 6371000 * c;
            return d;
        }

        private double Deg2Rad(double input)
        {
            return input * Math.PI / 180.0;
        }
    }
}
