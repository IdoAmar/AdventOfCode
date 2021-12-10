using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
namespace Stage1Part2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/1/input", cookieValue);
            int result = GetHigherThenTriosCount(input);
            Console.WriteLine("The result is : " + result);
        }
        public static int GetHigherThenTriosCount(string str)
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
