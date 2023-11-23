namespace OpenSportsPlatform.Lib.Services.Contract;

public interface ICalculateWorkoutStatisticsService
{
    Task Calculate(int workoutid);
}