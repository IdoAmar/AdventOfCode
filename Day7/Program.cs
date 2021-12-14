using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
namespace Day7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/7/input", cookieValue);
            int result1 = GetMinimalFuelSpentEffiecient(input);
            int result2 = GetMinimalFuelSpentWithCrabEngineering(input);
            Console.WriteLine("The result for part 1 is : " + result1);
            Console.WriteLine("The result for part 2 is : " + result2);
        }

        public static int GetMinimalFuelSpentEffiecient(string str)
        {
            var parsedInput = ParseString(str);
            var median = parsedInput.OrderBy(e => e).ElementAt(parsedInput.Count() / 2);
            return parsedInput.Sum(e => Math.Abs(e - median));
        }

        public static int GetMinimalFuelSpent(string str)
        {
            var parsedInput = ParseString(str);
            var options = parsedInput.GroupBy(e => e);
            var ans = parsedInput
                .Select(e => options
                    .Sum(ie => Math.Abs(ie.Key - e) * ie.Count()));
            return ans.Min();
        }
        public static int GetMinimalFuelSpentWithCrabEngineering(string str)
        {
            var parsedInput = ParseString(str);
            var options = parsedInput.GroupBy(e => e);
            var ans = parsedInput
                .Select(e => options
                    .Sum(ie => (Math.Abs(ie.Key - e) + 1) * Math.Abs(ie.Key - e) * ie.Count() / 2));
            return ans.Min();
        }

        public static IEnumerable<int> ParseString(string str) =>
            str.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(e => Int32.Parse(e));
    }
}
