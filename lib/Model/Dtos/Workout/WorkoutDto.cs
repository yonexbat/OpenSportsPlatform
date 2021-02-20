﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.Workout
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public float? DistanceInKm { get; set; }
        public float? DurationInSec { get; set; }

        public float? CaloriesInKCal { get; set; }

        public float? AscendInMeters { get; set; }
        public float? DescendInMeters { get; set; }

        public float? HeartRateAvgBpm { get; set; }
        public float? HeartRateMaxBpm { get; set; }

        public string Sport { get; set; }

        public IList<SampleDto> Samples { get; set; }
    }
}
