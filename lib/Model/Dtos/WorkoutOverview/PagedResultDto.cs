namespace OpenSportsPlatform.Lib.Model.Dtos.WorkoutOverview;

public class PagedResultDto<T>
{
    public IList<T> Data { get; init; } = null!;
    public int Count { get; init; } = default!;
}