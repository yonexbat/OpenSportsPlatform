using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface ICurrentDateTimeService
    {
        DateTimeOffset GetCurrentTime();
    }
}
