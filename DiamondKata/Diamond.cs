using System.Collections.Generic;
using System;

namespace DiamondKata
{
    public class Diamond
    {
        public static List<string> CreateDiamond(char c)
        {
            var result = new List<string>();
            if (Char.IsLetter(c))
            {
                for (var i = 'a'; i <= Char.ToLower(c); i++)
                {
                    result.Add("");
                }
            }

            return result;
        }
    }
}