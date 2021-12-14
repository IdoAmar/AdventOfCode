using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Day3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/3/input", cookieValue);
            int firstPartResult = GetPowerConsumption(input);
            int secondPartResult = GetLifeSupportRating(input);
            Console.WriteLine("First part result is : " + firstPartResult);
            Console.WriteLine("Second part result is : " + secondPartResult);
        }
        #region Part1
        public static int GetPowerConsumption(string str)
        {
            var parsedInput = str.Trim().Split("\n").Select(s => s.Select(c => c - '0'));
            var powArray = parsedInput
                .Aggregate(parsedInput.ElementAt(0), (a, c) => a.Zip(c, (e1, e2) => e1 + e2))
                .Select(e => parsedInput.Count() - e < parsedInput.Count() / 2 ? 1 : 0)
                .Reverse()
                .Select((e, i) => (int)Math.Pow(2, i) * e);
            return ((int)Math.Pow(2, powArray.Count()) - 1 - powArray.Sum()) * powArray.Sum();
        }
        #endregion

        #region Part2
        public static int GetLifeSupportRating(string str)
        {
            var parsedInput = str.Trim().Split("\n");
            List<string> mostList = parsedInput.ToList();
            List<string> leastList = parsedInput.ToList();

            for (int i = 0; i < parsedInput.First().Count(); i++)
            {
                if (mostList.Count != 1)
                    mostList = FilterMostByBit(mostList, i);
                if (leastList.Count != 1)
                    leastList = FilterLeastByBit(leastList, i);
            }
            return StringOfBinaryToNumber(mostList.First()) * StringOfBinaryToNumber(leastList.First());
        }
        public static List<string> FilterLeastByBit(List<string> numbers, int bitIndex)
        {
            List<string> oneList = new();
            List<string> zeroList = new();
            foreach (var number in numbers)
            {
                (number[bitIndex] == '0' ? zeroList : oneList).Add(number);
            }
            return (zeroList.Count <= oneList.Count) ? zeroList : oneList;
        }
        public static List<string> FilterMostByBit(List<string> numbers, int bitIndex)
        {
            List<string> oneList = new();
            List<string> zeroList = new();
            foreach (var number in numbers)
            {
                (number[bitIndex] == '0' ? zeroList : oneList).Add(number);
            }
            return (zeroList.Count <= oneList.Count) ? oneList : zeroList;
        }
        public static int StringOfBinaryToNumber(string binary)
        {
            return binary.Reverse().Select((e, i) => (int)Math.Pow(2, i) * (e - '0')).Sum();
        }
        #endregion
    }
}
