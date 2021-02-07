using OpenSportsPlatform.Lib.Model.Dtos.Statistics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IStatisticsService
    {
        Task<StatisticsDto> GetStatistics(GetStatisticsDto dto);
    }
}
