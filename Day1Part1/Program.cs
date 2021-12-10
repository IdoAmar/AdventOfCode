using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace Stage1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await getInputFromUrl("https://adventofcode.com/2021/day/1/input",cookieValue);
            int result = GetHigherThenCount(input);
            Console.WriteLine("The result is : " + result);
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
            return parsedInput.Select((e, i) => i > 0 ?
                        e > parsedInput.ElementAt(i - 1) ? true : false
                        : false).Count((e) => e == true);
        }
    }
}
