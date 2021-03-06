using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Day5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/5/input", cookieValue);
            int firstPartResult = GetDangerousSpots(input);
            int secondPartResult = GetDangerousSpotsWithDiagonals(input);
            Console.WriteLine("First part result is : " + firstPartResult);
            Console.WriteLine("Second part result is : " + secondPartResult);
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

        public static int GetDangerousSpotsWithDiagonals(string str)
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
                else
                {
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
                    else
                    {
                        // positive incline diagonal
                        if ((e.y2 - e.y1) / (e.x2 - e.x1) == 1)
                        {
                            var startY = Math.Min(e.y1, e.y2);
                            var endY = Math.Max(e.y1, e.y2);
                            var startX = Math.Min(e.x1, e.x2);
                            var endX = Math.Max(e.x1, e.x2);
                            for (int i = 0; i <= endX - startX; i++)
                            {
                                matrix[startX + i, startY + i]++;
                                if (matrix[startX + i, startY + i] == 2)
                                    dangerousSpots++;
                            }
                        }
                        // negative incline diagonal
                        if ((e.y2 - e.y1) / (e.x2 - e.x1) == -1)
                        {
                            var startY = Math.Max(e.y1, e.y2);
                            var endY = Math.Min(e.y1, e.y2);
                            var startX = Math.Min(e.x1, e.x2);
                            var endX = Math.Max(e.x1, e.x2);
                            for (int i = 0; i <= endX - startX; i++)
                            {
                                matrix[startX + i, startY - i]++;
                                if (matrix[startX + i, startY - i] == 2)
                                    dangerousSpots++;
                            }
                        }
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
