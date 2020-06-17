using FsCheck;
using Xunit;
using FsCheck.Xunit;
using DiamondKata;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DiamondKata.PropertyBasedTests
{
    public class Properties
    {
        // Already setup to use a custom Generator of Valid Characters: a-z or A-Z
        //[Property(Verbose = true, Arbitrary = new[] { typeof(ValidCharacters) })]
        [Property(Verbose = true, Arbitrary=new [] {typeof(InValidCharacters)})]
        public void AllInvalidCharactersShouldReturnAnEmptyDiamond(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);
            Assert.Equal(new List<string>(), diamond);
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        // [Theory]
        // [InlineData('b')]
        public void AllValidCharactersShouldReturnADiamondWithTheRightAmountOfRows(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);

            var letterIndex = ((int)letter.ToString().ToLower()[0] - (int)'a');
            var expectedNumRows = letterIndex * 2 + 1;
            Assert.Equal(expectedNumRows, diamond.Count);
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        public void MiddleRowHasInputLetter(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);

            var middleRow = diamond[(diamond.Count + 1) / 2 - 1];
            var firstCharacter = middleRow[0];
            var lastCharacter = middleRow[^1];
            Assert.Equal(letter, firstCharacter);
            Assert.Equal(letter, lastCharacter);
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        public void EachRowContainsExpectedNumberOfCharacters(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);

            var letterIndex = ((int)letter.ToString().ToLower()[0] - (int)'a');
            var expectedNumCharacters = letterIndex * 2 + 1;

            foreach (var row in diamond)
            {
                Assert.Equal(expectedNumCharacters, row.Length);
            }
        }

        [Property(Verbose = true, Arbitrary=new [] {typeof(ValidCharacters)})]
        public void DiamondShouldHaveLeftAndRightSymmetry(char letter)
        {
            var diamond = Diamond.CreateDiamond(letter);

            foreach (var row in diamond)
            {
                var left = row.Substring(0, ((row.Length+1)/2)-1);
                var right = row.Substring((row.Length+1)/2);

                var reversedRight = right.ToCharArray().ToList();
                reversedRight.Reverse();

                Console.WriteLine($"{row} => {left} and {new string(reversedRight.ToArray())}");
                Assert.Equal(reversedRight.ToArray(), left.ToCharArray());
            }
        }

        // todo: bottom and top are horizontal symmetrical

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