using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
namespace Day2Part2
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
            var ans = parsedInput
                .Select(e => (verb: e.Split(" ")[0],amount: Int32.Parse(e.Split(" ")[1])))
                .Aggregate((horizontal: 0, depth: 0, aim: 0), 
                    (a, c) => c.verb == "up" ? 
                    (a.horizontal, a.depth,a.aim -= c.amount) :
                    c.verb == "down" ?
                    (a.horizontal, a.depth, a.aim += c.amount):
                    (a.horizontal += c.amount, a.depth += a.aim * c.amount, a.aim));
            return ans.horizontal * ans.depth;
        }
    }
}
