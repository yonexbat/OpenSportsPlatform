using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.Workout
{
    public class SampleDto
    {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public float? AltitudeInMeters { get; set; }
        public float? HeartRateBpm { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
