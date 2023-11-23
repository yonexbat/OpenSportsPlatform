namespace OpenSportsPlatform.Lib.Util;

public static class DateUtil
{
    public static DateTimeOffset FirstOfMonth(this DateTimeOffset input)
    {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(input.Year, input.Month, 1, 0, 0, 0, input.Offset);
        return dateTimeOffset;
    }

    public static DateTimeOffset LastOfMonth(this DateTimeOffset input)
    {
        return input.FirstOfMonth().AddMonths(1).AddDays(-1);
    }
}