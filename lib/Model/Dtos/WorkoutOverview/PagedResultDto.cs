﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview
{
    public class PagedResultDto<T>
    {
        public IList<T> Data { get; init; } = null!;
        public int Count { get; init; } = default!;
    }
}
