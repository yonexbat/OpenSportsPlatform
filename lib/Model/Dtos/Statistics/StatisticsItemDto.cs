namespace OpenSportsPlatform.Lib.Model.Dtos.Statistics;

public class StatisticsItemDto
{
    public string SportsCategoryName { get; init; } = null!;

    public virtual float? DistanceInKm { get; init; }
}