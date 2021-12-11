using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using System.Collections.Generic;
namespace Day3Part2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/3/input", cookieValue);
            int result = GetLifeSupportRating(input);
            Console.WriteLine("The result is : " + result);
        }

        public static int GetLifeSupportRating(string str)
        {
            var parsedInput = str.Trim().Split("\n");
            List<string> mostList = parsedInput.ToList();
            List<string> leastList = parsedInput.ToList();

            for (int i = 0; i < parsedInput.First().Count(); i++)
            {
                if(mostList.Count != 1)
                    mostList = FilterMostByBit(mostList, i);
                if(leastList.Count != 1)
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
    }
}
