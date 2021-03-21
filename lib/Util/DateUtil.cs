using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Util
{
    public static class DateUtil
    {
        public static DateTime? AsUtc(this DateTime? input)
        {
            if(!input.HasValue)
            {
                return null;
            }

            switch(input.Value.Kind)
            {
                case DateTimeKind.Utc:
                    return input;
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(input.Value, DateTimeKind.Utc);
                case DateTimeKind.Local:
                    return input.Value.ToUniversalTime();
                default:
                    throw new ArgumentException($"Kind {input.Value.Kind} not supported");
            }
        }
    }
}
