using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Stage1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/1/input",cookieValue);
            int result = GetHigherThenCount(input);
            Console.WriteLine("The result is : " + result);
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
