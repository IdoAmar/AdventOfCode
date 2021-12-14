using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Day2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/2/input", cookieValue);
            int firstPartResult = GetFinalPosition(input);
            int secondPartResult = GetAimedFinalPosition(input);
            Console.WriteLine("First part result is : " + firstPartResult);
            Console.WriteLine("Second part result is : " + secondPartResult);
        }

        public static int GetFinalPosition(string str)
        {
            var parsedInput = str.Trim().Split("\n");
            var pairs = parsedInput
                .GroupBy(x => x.Split(" ")[0])
                .ToDictionary(g => g.Key, g => g.Select(e => Int32.Parse(e.Split(" ")[1])).Sum());
            return (pairs["down"] - pairs["up"]) * pairs["forward"];
        }

        public static int GetAimedFinalPosition(string str)
        {
            var parsedInput = str.Trim().Split("\n");
            var ans = parsedInput
                .Select(e => (verb: e.Split(" ")[0], amount: Int32.Parse(e.Split(" ")[1])))
                .Aggregate((horizontal: 0, depth: 0, aim: 0),
                    (a, c) => c.verb == "up" ?
                    (a.horizontal, a.depth, a.aim -= c.amount) :
                    c.verb == "down" ?
                    (a.horizontal, a.depth, a.aim += c.amount) :
                    (a.horizontal += c.amount, a.depth += a.aim * c.amount, a.aim));
            return ans.horizontal * ans.depth;
        }
    }
}
