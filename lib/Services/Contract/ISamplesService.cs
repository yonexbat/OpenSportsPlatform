using OpenSportsPlatform.Lib.Model.Dtos.Workout;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface ISamplesService
{
    Task<IAsyncEnumerable<SampleDto>> GetSamples(int workoutId);
}