using System.Globalization;

namespace app.Filters;

public class SpecificDateTimeFilter : AbstractFilter
{
    private string _pattern = "dd.MM.yyyy";
    private DateTime _start;
    private DateTime _end;
    public SpecificDateTimeFilter(DateTime start, DateTime end)
    {
        _start = start;
        _end = end;
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
