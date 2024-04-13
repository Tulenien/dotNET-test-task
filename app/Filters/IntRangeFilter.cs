namespace app.Filters;

public class IntRangeFilter : AbstractFilter
{
    private int _low;
    private int _high;

    public IntRangeFilter(int low, int high)
    {
        _low = low;
        _high = high;
    }

    protected override bool Evaluate(object request)
    {
        if ((int)request < _low || (int)request > _high)
        {
            return false;
        }
        return true;
    }
}
