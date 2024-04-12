using System.Text.RegularExpressions;

namespace app.Filters;

public class LineFormatFilter : AbstractFilter
{
    private string _pattern;
    public LineFormatFilter(string pattern)
    {
        _pattern = pattern;
    }
    
    protected override bool Evaluate(object request)
    {
        return Regex.IsMatch(request as string, _pattern);
    }
}
