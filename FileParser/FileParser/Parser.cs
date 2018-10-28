using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileParser
{
    public static class Parser
    {
        private static string _path;

        public static void SetFilePath(string path)
        {
            _path = path;
        }

        public static IEnumerable<Task<int>> ParseAndLogIfNeeded()
        {
            try
            {
                return ParseFile();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to parse the file: {e.Message}");
                return Enumerable.Empty<Task<int>>();
            }
        }

        public static IEnumerable<Task<int>> ParseFile()
        {
            if (string.IsNullOrEmpty(_path)) throw new ArgumentNullException("_path");

            using (var file = File.Open(_path, FileMode.Open))
            {
                using (var reader = new StreamReader(file))
                {
                    var numberOfEntries = int.Parse(reader.ReadLine());

                    for (int entry = 0; entry < numberOfEntries; entry++)
                    {
                        yield return ReadAndParseAsync(reader);
                    }
                }
            }
        }

        private static async Task<int> ReadAndParseAsync(StreamReader reader)
        {
            var line = await reader.ReadLineAsync();
            return int.Parse(line);
        }
    }
}
