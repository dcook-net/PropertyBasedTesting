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

            var count = letter.ToInt();

            var result = new List<string>(Enumerable
                .Range(1, count)
                .Select(x => x == 1 || x == count ? "A" : " "));
            
            return result;
        }
    }
    
    public static class CharExtensions
    {
        public static int ToInt(this char letter)
        {
            return letter.ToString().ToUpper().ToCharArray().First() - 64;
        }
    }
}