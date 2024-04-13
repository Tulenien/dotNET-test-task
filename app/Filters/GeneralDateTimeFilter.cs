using System.Globalization;

namespace app.Filters;

public class GeneralDateTimeFilter : AbstractFilter
{
    private string _pattern;
    public GeneralDateTimeFilter(string pattern)
    {
        _pattern = pattern;
    }

    protected override bool Evaluate(object request)
    {
        bool result = false;
        DateTime dateTime;

        if (DateTime.TryParseExact(request as string, _pattern, new CultureInfo("en-US"),
            DateTimeStyles.None, out dateTime))
        {
            if (ValidateYears(dateTime.Year) && ValidateMonths(dateTime.Month) &&
                ValidateDays(ref dateTime) && ValidateHours(dateTime.Hour) && 
                ValidateMinutes(dateTime.Minute) && ValidateSeconds(dateTime.Second))
            {
                result = true;
            }
        }
        return result;
    }

    private static bool ValidateYears(int year)
    {
        // Year 1951 is first commercial computer purchase year 
        return year > 1951 && year < DateTime.Now.Year + 1;
    }

    private static bool ValidateMonths(int month)
    {
        return month > 0 && month < 13;
    }

    private static bool ValidateDays(ref DateTime dateTime) {
        int expected = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        int day = dateTime.Day;
        return day > 0 && day < expected + 1;
    }

    private static bool ValidateHours(int hour)
    {
        return hour > -1 && hour < 24;
    }

    private static bool ValidateMinutes(int minute)
    {
        return minute > -1 && minute < 60;
    }

    private static bool ValidateSeconds(int second)
    {
        return second > -1 && second < 60;
    }
}
