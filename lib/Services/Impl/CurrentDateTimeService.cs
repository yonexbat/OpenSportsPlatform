using OpenSportsPlatform.Lib.Services.Contract;

namespace OpenSportsPlatform.Lib.Services.Impl;

class CurrentDateTimeService : ICurrentDateTimeService
{
    public DateTimeOffset GetCurrentTime()
    {
        return DateTimeOffset.UtcNow;
    }
}