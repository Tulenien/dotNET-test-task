namespace app.Filters;

public interface IFilter
{
    IFilter SetNext(IFilter filter);

    object? Check(object request);
}
