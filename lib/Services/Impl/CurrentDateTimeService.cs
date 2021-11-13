using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    class CurrentDateTimeService : ICurrentDateTimeService
    {
        public DateTimeOffset GetCurrentTime()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
