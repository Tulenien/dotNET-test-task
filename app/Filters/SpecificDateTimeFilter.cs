using System.Globalization;

namespace app.Filters;

public class SpecificDateTimeFilter : AbstractFilter
{
    private DateTime _start;
    private DateTime _end;
    private string _pattern;

    public SpecificDateTimeFilter(DateTime start, DateTime end, string pattern)
    {
        _start = start;
        _end = end;
        _pattern = pattern;
    }

    protected override bool Evaluate(object request)
    {
        DateTime time;
        if (DateTime.TryParseExact(request as string, _pattern, new CultureInfo("en-US"),
            DateTimeStyles.None, out time))
        {
            if (time.CompareTo(_start) >= 0 && time.CompareTo(_end) < 0)
            {
                return true;
            }
        }
        return false;
    }
}
