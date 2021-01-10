﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview
{
    public class WorkoutOverviewItemDto
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Sport { get; set; }
    }
}
