using OpenSportsPlatform.Lib.Util;
using System;
using Xunit;

namespace unittests;

public class DateHelperTests
{
    [Theory]
    [InlineData("Mon Dec 17 11:30:17 UTC 2018", 2018, 12, 17, 11, 30, 17)]
    [InlineData("Sun Mar 13 13:00:06 UTC 2011", 2011,  3, 13, 13, 0, 6)]
    [InlineData("2011 Mar 13 13:00:06", 2011, 3, 13, 13, 0, 6)]
    public void TestParseStrangeEndomondoDate(string dateString, int year, int month, int day, int hh, int mm, int ss)
    {
        var res = DateHelper.ParseStrangeEndomondoDate(dateString);
        DateTime expected = new DateTime(year, month, day, hh, mm, ss, DateTimeKind.Utc);
        Assert.Equal(expected, res.Value);
    }

    [Fact]
    public void ParseStrangeEndomondoDate_Null()
    {
        var res = DateHelper.ParseStrangeEndomondoDate(null!);
        Assert.Null(res);
    }

    [Fact]
    public void ParseStrangeEndomondoDate_WrongFormat()
    {
        try
        {
            var res = DateHelper.ParseStrangeEndomondoDate("abadsfrewr");
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsType<ArgumentException>(ex);
        }
    }
}