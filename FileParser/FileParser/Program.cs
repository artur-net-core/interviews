using System.Linq;

namespace FileParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.SetFilePath("FileWithNumbers.txt");
            var lines = Parser.ParseAndLogIfNeeded();
            var results = lines.Select(l => l.Result);
            var numbers = results.ToList();
        }        
    }
}
