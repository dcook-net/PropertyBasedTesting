using System.Collections.Generic;
using FsCheck;
using Xunit;
using FsCheck.Xunit;

namespace DiamondKata.PropertyBasedTests
{
    public class Properties
    {
        public class ValidCharacters
        {
            public static Arbitrary<char> Char()
            {
                return Arb.Default.Char().Filter(c => 'A' <= c && c <= 'Z');
            }
        }
        
        [Property(Arbitrary=new [] {typeof(ValidCharacters)})]
        public void Test1(char letter)
        {
            var result = Diamond.Create(letter);
            
            
        }
    }
}