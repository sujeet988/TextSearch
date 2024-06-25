namespace TextSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {

            // this we can ask from user
            bool reverseSearch = false;
            bool caseInsensitive = false;
            string wordtoSearch = "Ping";

            Console.WriteLine("Search from file");
            // using read file from system
            ReadFileFromSystem(wordtoSearch,reverseSearch, caseInsensitive);

            // read from web
            Console.WriteLine("Search from web");
             wordtoSearch = "Lorem";

            ProcessUrl("https://www.lipsum.com/", wordtoSearch, reverseSearch, caseInsensitive);

            Console.ReadLine();
        }

        static void ReadFileFromSystem(string wordtosearch ,bool reverseSearch,bool caseInsensitive)
        {
            List<string> sources = new List<string>();

            // taking default path of file c drive:
            string filePath = @"C:\\Users\\sujee\\source\\repos\\TextSearch\\Files\test.txt";
            sources.Add(filePath);

            // we can take this from user

            // Validate input
            if (sources.Count == 0)
            {
                Console.WriteLine("Invalid input format.");
                return;
            }

            StringComparison comparison = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            foreach (string source in sources)
            {
                try
                {

                    foreach (string target in sources)
                    {
                        ProcessFile(target, wordtosearch, reverseSearch, caseInsensitive);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {source}: {ex.Message}");
                }
            }
        }

        static void ProcessFile(string filePath, string word, bool reverseSearch, bool caseInsensitive)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int count = 0;
                foreach (string line in lines)
                {
                    count++;
                    bool match = caseInsensitive ?
                        line.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0 :
                        line.Contains(word);

                    if (match && !reverseSearch || !match && reverseSearch)
                    {
                      
                        Console.WriteLine($"Line {count}: {word}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
            }
        }

        static void ProcessUrl(string url, string word, bool reverseSearch, bool caseInsensitive)
        {
            try
            {
                var wc = new WebClient();
                string webData = wc.DownloadString(url);
                string[] lines = webData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int countLines = 0;
                foreach (string line in lines)
                {
                    countLines++;
                    bool match = caseInsensitive ?
                        line.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0 :
                        line.Contains(word);

                    if (match && !reverseSearch || !match && reverseSearch)
                    {
                        Console.WriteLine($"Line {countLines}: {word}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading or processing {url}: {ex.Message}");
            }
        }


    }

}