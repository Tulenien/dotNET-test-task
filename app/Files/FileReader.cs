namespace app.Files;

public static class FileReader
{
    public static List<string> ReadFileContent(string path)
    {
        List<string> result = [];
        try
        {
            using StreamReader sr = new(path);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                result.Add(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return result;
    }
}
