using System;
using System.Linq;
using System.Collections.Generic;

namespace DiamondKata
{
    public class Diamond
    {
        private readonly int initialLetter = 65;

        public IEnumerable<string> ConvertToDiamond(char letter)
        {
            if(!char.IsLetter(letter))
                return new List<string>();

            var upperChar = char.ToUpper(letter);
            var diamondList = new List<string>();
            var extraSpacing = 2;
            var lengthBetweenFirstAndSecondLetter = upperChar - initialLetter + extraSpacing;

            var range = Enumerable.Range(initialLetter, lengthBetweenFirstAndSecondLetter);
            var content = range
                .Select(x => ".")
                .Aggregate((x, y) => x + y);

            for (int i = initialLetter; i <= upperChar; i++)
            {
                diamondList.Add(content);
            }

            for (int i = upperChar - 1; i >= initialLetter; i--)
            {
                diamondList.Add(content);
            }

            return diamondList;
        }
    }
}
