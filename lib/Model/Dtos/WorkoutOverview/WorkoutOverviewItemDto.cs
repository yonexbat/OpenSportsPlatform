using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview
{
    public class WorkoutOverviewItemDto
    {
        public int Id { get; init; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public float? DistanceInKm { get; set; }
        public float? DurationInSec { get; set; }
        public string? Sport { get; set; }
        public IList<string>? Tags { get; set; }
    }
}
