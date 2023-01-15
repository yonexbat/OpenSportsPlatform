using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.Statistics
{
    public class StatisticsDto
    {
        public IList<StatisticsItemDto>? MonthItems { get; set; }

        public IList<StatisticsItemDto>? YearItems { get; set; }

        public IList<MonthValueItemDto>? RunningLast12Months { get; set; }
    }
}
