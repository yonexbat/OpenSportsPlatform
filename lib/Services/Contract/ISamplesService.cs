using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using OpenSportsPlatform.Lib.Model.Entities;

namespace OpenSportsPlatform.Lib.Services.Contract;

public interface ISamplesService
{
    Task<IAsyncEnumerable<SampleDto>> GetSamples(int workoutId);
}