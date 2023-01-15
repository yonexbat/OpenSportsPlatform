using OpenSportsPlatform.Lib.Model.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.Workout
{
    public class EditWorkoutDto
    {
        public int? Id { get; set; }
        public int? SportsCategoryId { get; set; }
        public string? Notes { get; set; }
        public IList<SelectItemDto>? SportsCategories { get; set; }
        public IList<SelectItemDto>? Tags { get; set; }
        public DateTimeOffset? FirstSampleTimestamp { get; set; }
        public DateTimeOffset? LastSampleTimestamp { get; set; }
        public double? Ticks { get; set; }
    }
}
