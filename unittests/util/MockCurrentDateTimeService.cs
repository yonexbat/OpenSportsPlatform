using OpenSportsPlatform.Lib.Services.Contract;
using System;

namespace unittests.util;

public class MockCurrentDateTimeService : ICurrentDateTimeService
{
    private DateTimeOffset _date;
    public MockCurrentDateTimeService(DateTimeOffset date)
    {
        _date = date;
    }
    public DateTimeOffset GetCurrentTime()
    {
        return _date;
    }
}