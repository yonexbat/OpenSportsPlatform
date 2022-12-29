using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.Workout
{
    public class SaveWorkoutDto
    {
        public int? Id { get; set; }
        public int? SportsCategoryId { get; set; }
        public string? Notes { get; set; }
        public string? Tag { get; set; }
    }
}
