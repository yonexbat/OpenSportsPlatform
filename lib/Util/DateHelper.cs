using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenSportsPlatform.Lib.Util
{
    public static class DateHelper
    {
        public static DateTime? ParseStrangeEndomondoDate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            else if(input.Length == 28 && input.Contains("UTC"))
            {
                string year = input.Substring(24, 4);
                string month = input.Substring(4, 3);
                string day = input.Substring(8, 2);
                string time = input.Substring(11, 8);

                string parsableString = $"{year} {month} {day} {time}";
                string format = "yyyy MMM dd HH:mm:ss";
                CultureInfo provider = CultureInfo.InvariantCulture;
                var res = DateTime.ParseExact(parsableString, format, provider, DateTimeStyles.AssumeUniversal);
                return res.ToUniversalTime();

            }
            else if (input.Length == 20 || input.Length == 21)
            {
                var res = DateTime.Parse(input, null, DateTimeStyles.AssumeUniversal);
                return res.ToUniversalTime();
            }

            throw new ArgumentException($"DateTime format not expected: {input}");
        }

    }
}
