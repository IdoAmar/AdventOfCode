using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Day3Part1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/3/input", cookieValue);
            int result = GetFinalPosition(input);
            Console.WriteLine("The result is : " + result);
        }

        public static int GetFinalPosition(string str)
        {
            var parsedInput = str.Trim().Split("\n").Select(s => s.Select(c => c - '0'));
            var powArray = parsedInput
                .Aggregate(parsedInput.ElementAt(0), (a, c) => a.Zip(c, (e1, e2) => e1 + e2))
                .Select(e => parsedInput.Count() - e < parsedInput.Count() / 2 ? 1 : 0)
                .Reverse()
                .Select((e, i) => (int)Math.Pow(2, i) * e);
            return ((int)Math.Pow(2, powArray.Count()) - 1 - powArray.Sum()) * powArray.Sum();
        }
    }
}
