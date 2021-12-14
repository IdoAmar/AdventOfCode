using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Day8
{
    class Program
    {
        public static Dictionary<string, int> numbersDefinitions = new() { { "abcefg", 0 }, { "cf", 1 }, { "acdeg", 2 }, { "acdfg", 3 }, { "bcdf", 4 }, { "abdfg", 5 }, { "abdefg", 6 }, { "acf", 7 }, { "abcdefg", 8 }, { "abcdfg", 9 } };

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/8/input", cookieValue);
            int result = GetAllNumbers(input);
            Console.WriteLine("The result is : " + result);
        }

        public static int GetEasyNumbersCount(string str)
        {
            var parsedInput = ParseInput(str).ToList();
            var numbers = parsedInput.Select(e => DecryptDataAndValues(e.encryptedData, e.values));
            return numbers.Select(e => e.Count(ie => (ie == 1 || ie == 4 || ie == 7 || ie == 8))).Sum();
        }

        public static int GetAllNumbers(string str)
        {
            var parsedInput = ParseInput(str).ToList();
            var numbers = parsedInput.Select(e => DecryptDataAndValues(e.encryptedData, e.values));
            return numbers.Select(e => Int32.Parse(e.Aggregate("", (a, c) => a + c))).Sum();
        }

        public static IEnumerable<int> DecryptDataAndValues(string[] encryptedData, string[] values)
        {
            var decryptor = decryptionDictionaryFactory(encryptedData);
            var decrypedValues = values.Select(e => e.Select(ie => decryptor[ie]).Aggregate("", (a, c) => a + c));
            return decrypedValues.Select(s => ParseToNumber(s));
        }

        public static Dictionary<char, char> decryptionDictionaryFactory(string[] encryptedData)
        {
            var reverseLookup = encryptedData.SelectMany(e => e).GroupBy(g => g).ToDictionary(g => g.Key, g => g.Count());
            var countersLookup = reverseLookup.Where(e => reverseLookup.Values.Count(ie => ie == e.Value) == 1).ToDictionary(d => d.Value, d => d.Key);


            Dictionary<char, char> decryptionLookup = new();
            decryptionLookup.Add(countersLookup[6], 'b');
            decryptionLookup.Add(countersLookup[4], 'e');
            decryptionLookup.Add(countersLookup[9], 'f');
            decryptionLookup.Add(
                encryptedData.First(e => e.Length == 2)
                             .Where(c => !decryptionLookup.Keys.Contains(c))
                             .Single(), 'c');
            decryptionLookup.Add(
                encryptedData.First(e => e.Length == 3)
                             .Where(c => !decryptionLookup.Keys.Contains(c))
                             .Single(), 'a');
            decryptionLookup.Add(
                encryptedData.First(e => e.Length == 4)
                             .Where(c => !decryptionLookup.Keys.Contains(c))
                             .Single(), 'd');
            decryptionLookup.Add(
                encryptedData.First(e => e.Length == 7)
                             .Where(c => !decryptionLookup.Keys.Contains(c))
                             .Single(), 'g');

            return decryptionLookup;
        }

        public static int ParseToNumber(string str) => numbersDefinitions[str.OrderBy(c => c).Aggregate("", (a, c) => a + c).Trim()];

        public static IEnumerable<(string[] encryptedData, string[] values)> ParseInput(string str) =>
            str.Trim()
               .Split("\n", StringSplitOptions.RemoveEmptyEntries)
               .Select(s => s.Split(" | ", StringSplitOptions.RemoveEmptyEntries))
               .Select(s => (s[0].Split(" ", StringSplitOptions.RemoveEmptyEntries), s[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)));
    }
    public static class Extentions
    {
        public static string RemoveCharactersFromString(this string str, char[] charactersToRemove)
        {
            return str.Where(c => charactersToRemove.Any(ic => ic == c)).Aggregate("", (a, c) => a + c);
        }
    }
}
