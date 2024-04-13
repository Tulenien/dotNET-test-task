
namespace app.Filters;

public static class FiltersFabric
{
    private const string _ipv4DateTimePattern = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3} \d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$";
    private const string _dateTimeFile = "yyyy-MM-dd HH:mm:ss";
    private const string _dateTimeArg = "dd.MM.yyyy HH:mm:ss";
    private const int _lowBorder = 0;
    private const int _highBorder = 255;

    public static IFilter CreateLogLineFilter(DateTime startTime, DateTime endTime)
    {
        IFilter lineFormatCheck = new LineFormatFilter(_ipv4DateTimePattern);
        IFilter generalDateTimeCheck = new GeneralDateTimeFilter(_dateTimeFile);
        return generalDateTimeCheck.SetNext(lineFormatCheck);
    }

    public static IFilter CreateLogLineFilter(DateTime startTime, DateTime endTime, string startAddress)
    {
        IFilter lineFormatCheck = new LineFormatFilter(_ipv4DateTimePattern);
        IFilter generalDateTimeCheck = new GeneralDateTimeFilter(_dateTimeFile);
        IFilter specificTimeCheck = new SpecificDateTimeFilter(startTime, endTime, _dateTimeFile);
        IFilter specificIPAddressCheck = new SpecificIPAdressFilter(startAddress);
        return specificIPAddressCheck.SetNext(specificTimeCheck)
            .SetNext(generalDateTimeCheck)
            .SetNext(lineFormatCheck);
    }

    public static IFilter CreateLogLineFilter(DateTime startTime, DateTime endTime,
        string startAddress, int mask)
    {
        IFilter lineFormatCheck = new LineFormatFilter(_ipv4DateTimePattern);
        IFilter generalDateTimeCheck = new GeneralDateTimeFilter(_dateTimeFile);
        IFilter specificTimeCheck = new SpecificDateTimeFilter(startTime, endTime, _dateTimeFile);
        IFilter specificIPAddressCheck = new SpecificIPAdressFilter(startAddress, mask);
        return specificIPAddressCheck.SetNext(specificTimeCheck)
            .SetNext(generalDateTimeCheck)
            .SetNext(lineFormatCheck);
    }

    public static IFilter CreateMaskFilter()
    {
        return new IntRangeFilter(_lowBorder, _highBorder);
    }

    public static IFilter CreateTimeIntervalFilter()
    {
        return new GeneralDateTimeFilter(_dateTimeArg);
    }
}
