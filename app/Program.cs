using app.Configuration;
using app.Files;
using app.Filters;

namespace app;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            List<string> result = FileReader.ReadFileContent("./TestFiles/CorrectLineFile.txt");
            DateTime start;
            DateTime.TryParse("2023.07.22 22:00:00", out start);
            DateTime end = DateTime.Now;
            IFilter filter = FiltersFabric.CreateLogLineFilter(start, end);
            Console.WriteLine(filter.Check(result[0]));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
