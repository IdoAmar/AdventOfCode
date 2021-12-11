using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using BoardsType = System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<string>>>;

namespace Day4Part1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the session cookie value");
            string cookieValue = Console.ReadLine();
            string input = await ScrapingUtilities.getInputFromUrl("https://adventofcode.com/2021/day/4/input", cookieValue);
            int result = GetFirstWinningBoardScore(input); 
            Console.WriteLine("The result is : " + result);
        }

        public static int GetFirstWinningBoardScore(string str)
        {
            (var results , var boards) = ParseInput(str);
            return CheckForWinningBoard(boards, results);
        }
        public static int CheckForWinningBoard(BoardsType boards, IEnumerable<string> results)
        {
            for (int i = 5; i < results.Count(); i++)
            {
                for (int j = 0; j < boards.Count(); j++)
                {
                    var res = CheckBoard(boards.ElementAt(j), results.Take(i));
                    if (res > 0)
                        return res;
                }
            }
            return 0;
        }
        public static int CheckBoard(IEnumerable<IEnumerable<string>> board, IEnumerable<string> results)
        {
            bool bingo = false;
            int score = 0;
            for (int i = 0; i < board.Count(); i++)
            {
                bool verticalBingo = true;
                bool horizontalBingo = true;
                for (int j = 0; j < board.ElementAt(i).Count(); j++)
                {
                    var verticalCell = board.ElementAt(j).ElementAt(i);
                    var horizontalCell = board.ElementAt(i).ElementAt(j);

                    if (!results.Contains(verticalCell))
                    {
                        verticalBingo = false;
                        score += Int32.Parse(verticalCell);
                    }
                    if (!results.Contains(horizontalCell))
                        horizontalBingo = false;
                }
                if(horizontalBingo || verticalBingo)
                {
                    bingo = true;
                }
            }
            int finalScore = score * Int32.Parse(results.Last());
            return bingo ? finalScore : 0;
        }
        public static (IEnumerable<string> results, BoardsType) ParseInput(string str)
        {
            var parsedInput = str.Trim().Split("\n\n");
            var results = parsedInput.First().Split(",");
            var boards = parsedInput
                .Skip(1)
                .Select(s =>
                    s.Split("\n")
                     .Select(inner =>
                        inner.Split(" ")
                             .Where(s => !String.IsNullOrWhiteSpace(s))));
            return (results, boards);
        }
    }
}
