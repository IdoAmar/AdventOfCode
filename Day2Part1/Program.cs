using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace Day2Part1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await getInputFromUrl("https://adventofcode.com/2021/day/2/input", cookieValue);
            int result = GetFinalPosition(input);
            Console.WriteLine("The result is : " + result);
        }
        public static async Task<string> getInputFromUrl(string url, string cookieValue)
        {
            var cookie = "session=" + cookieValue;
            WebClient wb = new WebClient();
            wb.Headers.Add(HttpRequestHeader.Cookie, cookie);
            return await wb.DownloadStringTaskAsync(url);
        }
        public static int GetFinalPosition(string str)
        {
            var parsedInput = str.Trim().Split("\n");
            var a = parsedInput
                .GroupBy(x => x.Split(" ")[0])
                .ToDictionary(g => g.Key, g => g.Select(e => Int32.Parse(e.Split(" ")[1])).Sum())
                ;
            return (a["down"] - a["up"]) * a["forward"];
        }

    }
}
