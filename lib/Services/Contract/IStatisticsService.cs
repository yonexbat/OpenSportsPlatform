using OpenSportsPlatform.Lib.Model.Dtos.Statistics;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface IStatisticsService
{
    Task<StatisticsDto> GetStatistics(GetStatisticsDto dto);
}