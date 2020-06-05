using FsCheck;
using Xunit;
using FsCheck.Xunit;
using DiamondKata;
using System.Collections.Generic;

namespace DiamondKata.PropertyBasedTests
{
    public class Properties
    {
        /// Already setup to use a custom Generator of Valid Characters: a-z or A-Z
        //[Property(Verbose = true, Arbitrary = new[] { typeof(ValidCharacters) })]
        [Property(Verbose = true, Arbitrary=new [] {typeof(InValidCharacters)})]
        public void OurFirstProperty(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);
            Assert.Equal(new List<string>(), diamond);
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        // [Theory]
        // [InlineData('b')]
        public void OurSecondProperty(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);
            
            var expectedNumRows = ((int)letter.ToString().ToLower()[0] - (int)'a') + 1;
            Assert.Equal(expectedNumRows, diamond.Count);
        }

        // Expected output for F:
        //     A     
        //    B B    
        //   C   C   
        //  D     D  
        // E       E 
        //F         F
        // E       E 
        //  D     D  
        //   C   C   
        //    B B    
        //     A    

        private class ValidCharacters
        {
            public static Arbitrary<char> Char()
            {
                return Arb.Default.Char()
                    .Filter(c => c >= 65 && c <= 122 && !(c >= 90 && c <= 97));
            }
        }

        private class InValidCharacters
        {
            public static Arbitrary<char> Char()
            {
                return Arb.Default.Char()
                    .Filter(c => c < 65 || c >= 123 || c > 90 && c < 97);
            }
        }
    }
}