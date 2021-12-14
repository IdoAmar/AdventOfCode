using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Day6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/6/input", cookieValue);
            long result = GetNumberOfFishesEfficient(input, 256);
            Console.WriteLine("The result is : " + result);
        }

        public static long GetNumberOfFishesEfficient(string str, int days)
        {
            var parsedInput = str.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
            var fishes = parsedInput.Select(e => Int64.Parse(e)).ToList();
            var fishBuckets = Enumerable.Repeat<long>(0, 9).Select<long, long>((e, i) => fishes.Count(e => e == i)).ToList();
            for (int i = 0; i < days; i++)
            {
                fishBuckets = ShiftLeft(fishBuckets);
                fishBuckets[6] += fishBuckets[8];
            }
            return fishBuckets.Sum(); ;
        }

        public static List<long> ShiftLeft(List<long> list)
        {
            List<long> newList = new(list);
            long temp = list[0];
            for (int i = 0; i < list.Count - 1; i++)
                newList[i] = newList[i + 1];
            newList[newList.Count - 1] = temp;
            return newList;
        }

        // naive way
        public static int GetNumberOfFishes(string str, int days)
        {
            var parsedInput = str.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
            var fishes = parsedInput.Select(e => Int32.Parse(e)).ToList();
            for (int i = days; i > 0; i--)
            {
                List<int> temporaryFishContainer = new();
                foreach (var fish in fishes)
                {
                    if (fish is 0)
                        temporaryFishContainer.Add(8);
                    temporaryFishContainer.Add(fish is 0 ? 6 : fish - 1);
                }
                fishes = temporaryFishContainer;
            }
            return fishes.Count;
        }
        // naive way with linq
        public static int GetNumberOfFishesLinq(string str, int days)
        {
            var parsedInput = str.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
            var fishes = parsedInput.Select(e => Int32.Parse(e));
            while (days > 0)
            {
                var fishToAdd = fishes.Count(e => e == 0);
                fishes = fishes.Select(e => e == 0 ? 6 : --e);
                fishes = fishes.Concat(Enumerable.Repeat(8, fishToAdd));
                days--;
            }
            return fishes.Count();

        }
    }
}
