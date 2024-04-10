namespace app.Filters;

public abstract class AbstractFilter : IFilter
{
    private IFilter? _nextFilter;
    public virtual object? Check(object request)
    {
        if (Evaluate(request))
        {
            if (_nextFilter != null)
            {
                return _nextFilter.Check(request);
            }
            else
            {
                return request;
            }
        }
        else
        {
            return null;
        }
    }

    public IFilter SetNext(IFilter filter)
    {
        _nextFilter = filter;
        return filter;
    }

    protected abstract bool Evaluate(object request);
}
