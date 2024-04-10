using app.Files;
using app.Filters;

namespace app;

class Program
{
    static void Main(string[] args)
    {
        const string pattern = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3} \d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$";
        try
        {
            List<string> result = FileReader.ReadFileContent("./TestFiles/CorrectLineFile.txt");
            IFilter filter = new LineFormatFilter(pattern);
            Console.WriteLine(filter.Check(result[0]));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
}
