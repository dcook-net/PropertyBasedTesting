using System;
using System.Collections.Generic;
using System.Linq;

namespace DiamondKata
{
    public class Diamond
    {
        public static IEnumerable<string> Create(char letter)
        {
            if (!char.IsLetter(letter))
                return new List<string>();

            var top = BuildTop(letter);

            return AddBottom(top);;
        }

        private static List<string> BuildTop(char letter)
        {
            var numberOfLetters = letter.ToInt();
            var rowLength = numberOfLetters * 2 - 1;
            var innerBufferLength = 1;

            Func<int> GetInnerBufferLength = () =>
            {
                var length = innerBufferLength;
                innerBufferLength += 2;
                return length;
            };

            return new List<string>(
                Enumerable
                    .Range(1, numberOfLetters)
                    .Select(x => x == 1 ? "A" : AddInnerBuffer(x.ToChar(), GetInnerBufferLength()))
                    .Select(x => AddOuterBuffer(x, x == "A" ? numberOfLetters - 1 : (rowLength - x.Length) / 2)));
        }

        private static IEnumerable<string> AddBottom(IReadOnlyCollection<string> top)
        {
            var bottom = top.Take(top.Count - 1).Reverse();
            
            var result = new List<string>();
            result.AddRange(top);
            result.AddRange(bottom);

            return result;
        }


        private static string AddInnerBuffer(char letter, int lengthOfBuffer)
        {
            var buffer = BuildBuffer(lengthOfBuffer);
            return $"{letter}{buffer}{letter}";
        }

        private static string AddOuterBuffer(string value, int length)
        {
            if (length <= 0) return value;
            
            var buffer = BuildBuffer(length);
                
            return $"{buffer}{value}{buffer}";
        }

        private static string BuildBuffer(int length)
        {
            var buffer = Enumerable
                .Range(1, length)
                .Select(x => " ")
                .Aggregate((a, b) => a + b);
            return buffer;
        }
    }
    
    public static class CharExtensions
    {
        public static int ToInt(this char letter)
        {
            return letter.ToString().ToUpper().ToCharArray().First() - 64;
        }
    }

    public static class IntExtensions
    {
        public static char ToChar(this int number)
        {
            return (char)(number + 64);
        }
    }
}