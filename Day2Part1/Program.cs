using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
namespace Day2Part1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/2/input", cookieValue);
            int result = GetFinalPosition(input);
            Console.WriteLine("The result is : " + result);
        }

        public static int GetFinalPosition(string str)
        {
            var parsedInput = str.Trim().Split("\n");
            var pairs = parsedInput
                .GroupBy(x => x.Split(" ")[0])
                .ToDictionary(g => g.Key, g => g.Select(e => Int32.Parse(e.Split(" ")[1])).Sum());
            return (pairs["down"] - pairs["up"]) * pairs["forward"];
        }
    }
}
