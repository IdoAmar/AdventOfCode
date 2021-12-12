using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
namespace Day5Part1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/5/input", cookieValue);
            int result = GetDangerousSpots(input);
            Console.WriteLine("The result is : " + result);
        }

        public static int GetDangerousSpots(string str)
        {
            var parsedInput = parseInput(str);
            var matrix = new int[999, 999];
            var dangerousSpots = 0;
            foreach (var e in parsedInput)
            {
                if (e.x1 == e.x2)
                {
                    var start = Math.Min(e.y1, e.y2);
                    var end = Math.Max(e.y1, e.y2);
                    for (int i = start; i <= end; i++)
                    {
                        matrix[e.x1, i]++;
                        if (matrix[e.x1, i] == 2)
                            dangerousSpots++;
                    }
                }
                if (e.y1 == e.y2)
                {
                    var start = Math.Min(e.x1, e.x2);
                    var end = Math.Max(e.x1, e.x2);
                    for (int i = start; i <= end; i++)
                    {
                        matrix[i, e.y1]++;
                        if (matrix[i, e.y1] == 2)
                            dangerousSpots++;
                    }
                }
            }
            return dangerousSpots;
        }

        public static IEnumerable<(int x1, int y1, int x2, int y2)> parseInput(string str)
        {
            return str
                .Trim()
                .Split("\n")
                .Select(s => s
                    .Split(new[] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(s => (x1: Int32.Parse(s[0]), y1: Int32.Parse(s[1]), x2: Int32.Parse(s[2]), y2: Int32.Parse(s[3])));
        }
    }
}
