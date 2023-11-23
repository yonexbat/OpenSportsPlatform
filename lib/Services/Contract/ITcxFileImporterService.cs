namespace OpenSportsPlatform.Lib.Services.Contract;

public interface ITcxFileImporterService
{
    Task ImportWorkout(Stream stream);
}