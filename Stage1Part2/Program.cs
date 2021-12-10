using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace Stage1Part2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await getInputFromUrl("https://adventofcode.com/2021/day/1/input", cookieValue);
            int result = GetHigherThenCount(input);
        }
        public static async Task<string> getInputFromUrl(string url, string cookieValue)
        {
            var cookie = "session=" + cookieValue;
            WebClient wb = new WebClient();
            wb.Headers.Add(HttpRequestHeader.Cookie, cookie);
            return await wb.DownloadStringTaskAsync(url);
        }
        public static int GetHigherThenCount(string str)
        {
            var parsedInput = str.Trim().Split("\n").Select(e => Int32.Parse(e));
            int length = parsedInput.Count();
            var transformedInput = 
                parsedInput.SkipLast(2)
                           .Select((e, i) => e + parsedInput.ElementAt(i + 1) + parsedInput.ElementAt(i + 2));
            return transformedInput.Select((e, i) => i > 0 ?
                                                e > transformedInput.ElementAt(i - 1) ? true : false
                                                : false).Count((e) => e == true);
        }
    }
}
