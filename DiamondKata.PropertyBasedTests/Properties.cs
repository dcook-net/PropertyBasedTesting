using System.Collections.Generic;
using System.Linq;
using FsCheck;
using Xunit;
using FsCheck.Xunit;

namespace DiamondKata.PropertyBasedTests
{
    public class Properties
    {
        private List<(char letter, int index, int lettersPerRow)> _expectations;
        
        public Properties()
        {
            _expectations = Init();
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(InValidCharacters)})]
        public void ShouldReturnEmptyCollectionForNonLetterInputs(char letter)
        {
            Assert.Equal(new List<string>(), Diamond.Create(letter));
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void ResultShouldContainExpectedNumberOfRows(char letter)
        {
            var result = Diamond.Create(letter);

            var expectedNumberOfRows = _expectations.Find(x => x.letter.ToString() == letter.ToString().ToUpper()).index;

            Assert.Equal(expectedNumberOfRows, result.Count());
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void FirstAndLastRowsShouldContainASingleA(char letter)
        {
            var result = Diamond.Create(letter).ToList();

            Assert.Equal("A", result.First().Trim());
            Assert.Equal("A", result.Last().Trim());
        }
        //
        // [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        // public void NumberOfRowsShouldEqualCharsPerRow(char letter)
        // {
        //     var result = Diamond.Create(letter).ToList();
        //
        //     var numbersOfRows = result.Count;
        //
        //     foreach (var row in result)
        //     {
        //         Assert.Equal(row.Length, numbersOfRows);
        //     }
        // }
        //
        // [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        // public void ShouldHaveTheCorrectNumberOfLettersPerRow(char letter)
        // {
        //     var result = Diamond.Create(letter).ToList();
        //
        //     var index = 0;
        //     var last = result.Count - 1;
        //     foreach (var row in result)
        //     {
        //         var expected = index == 0 || index == last ? 1 : 2;
        //         Assert.Equal(row.Trim().ToCharArray().Length, expected);
        //         index++;
        //     }
        // }

        // [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        // public void NumberOfRowsShouldBeCorrect(char letter)
        // {
        //     var result = Diamond.Create(letter).ToList();
        //
        //     var index = 0;
        //     foreach (var row in result)
        //     {
        //         var (c, i, lettersPerRow) = _expectations[index];
        //
        //         var letters = row.Trim().ToCharArray();
        //         Assert.Equal(letters.Length, lettersPerRow);
        //         foreach (var l in letters)
        //         {
        //             Assert.Equal(l, c);
        //         }
        //         
        //         index++;
        //     }
        // }

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

        private static List<(char letter, int index, int lettersPerRow)> Init()
        {
            return Enumerable.Range(1, 26).Select(x => ((char)(x + 64), x, x == 1 ? 1 : 2)).ToList();
        }

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