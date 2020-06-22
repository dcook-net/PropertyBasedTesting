using FsCheck;
using System;
using Xunit;
using FsCheck.Xunit;
using DiamondKata;
using System.Linq;
using System.Collections.Generic;

namespace DiamondKata.PropertyBasedTests
{
    public class Properties
    {
        /// Already setup to use a custom Generator of Valid Characters: a-z or A-Z
        // [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        [Property(Verbose = true, Arbitrary=new [] {typeof(InValidCharacters)})]
        public void InvalidCharacters_Should_ReturnEmptyResponse(char letter)
        {
            Diamond diamond = new Diamond();
            var diamondCollection = diamond.ConvertToDiamond(letter);

            Assert.Equal(Enumerable.Empty<string>(), diamondCollection);
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        public void ValidCharacters_Should_HaveOddNumberOfRows(char letter)
        {
            Diamond diamond = new Diamond();
            var diamondCollection = diamond.ConvertToDiamond(letter);

            Assert.True(diamondCollection.Count() % 2 == 1);
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        public void ValidCharacters_Should_ResultInDiamondOfExpectedHeight(char letter)
        {
            var upperChar = char.ToUpper(letter);
            var letterIndex = (upperChar - 64);
            var expected = letterIndex * 2 - 1;
            Diamond diamond = new Diamond();

            var diamondCollection = diamond.ConvertToDiamond(upperChar);

            Assert.Equal(expected, diamondCollection.Count());
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        // [Theory]
        // [InlineData('B')]
        public void ValidCharacters_Should_ResultInEachRowBeingSameLengthAsDiamondHeight(char letter)
        {
            var letterIndex = (letter - 64);
            var expected = letterIndex * 2 - 1;
            Diamond diamond = new Diamond();

            var diamondCollection = diamond.ConvertToDiamond(letter);

            Console.WriteLine("Look at this");
            Console.WriteLine(expected);
            Console.WriteLine(diamondCollection.First().Count());

            Assert.True(diamondCollection.All(x => x.Count() == expected));
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