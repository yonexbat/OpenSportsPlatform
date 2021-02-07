using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos.Statistics
{
    public class StatisticsItemDto
    {
       public string SportsCategoryName { get; set; }

       public virtual float? DistanceInKm { get; set; }
    }
}
