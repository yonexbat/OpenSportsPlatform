using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unittests.util
{
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
}
