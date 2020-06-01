using System.Collections.Generic;
using System.Linq;
using FsCheck;
using Xunit;
using FsCheck.Xunit;

namespace DiamondKata.PropertyBasedTests
{
    public class Properties
    {
        private readonly List<(char Letter, int LettersPerRow, int ExpectedNumberOfRows)> _expectations;
        
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

            var expectations = GetExpectationsForLetter(letter);

            Assert.Equal(expectations.expectedNumberOfRows, result.Count());
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void FirstAndLastRowsShouldContainASingleA(char letter)
        {
            var result = Diamond.Create(letter).ToList();

            Assert.Equal("A", result.First().Trim());
            Assert.Equal("A", result.Last().Trim());
        }
 
        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void NumberOfRowsShouldEqualCharsPerRow(char letter)
        {
            var result = Diamond.Create(letter).ToList();
        
            var numbersOfRows = result.Count;
        
            foreach (var row in result)
            {
                Assert.Equal(numbersOfRows, row.Length);
            }
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void ShouldBeHorizontallySymmetrical(char letter)
        {
            var result = Diamond.Create(letter).ToList();

            foreach (var row in result)
            {
                var (left, right) = row.SplitInTwo();

                Assert.Equal(left, right);
            }
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void ShouldHaveTheCorrectNumberOfLettersPerRow(char letter)
        {
            var result = Diamond.Create(letter).ToList();

            foreach (var row in result)
            {
                var chars = row.Trim().ToCharArray().Where(char.IsLetter).ToList();
                var expectations = GetExpectationsForLetter(chars.First());
                Assert.Equal(expectations.lettersPerRow, chars.Count);
            }
        }

        //This didn't force us to add any behaviour!
        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void ResultShouldBeHorizontallySymmetrical(char letter)
        {
            var results = Diamond.Create(letter).ToList();
            var resultsFlippedHorizontally = results;
            resultsFlippedHorizontally.Reverse();

            for (var i = 0; i < results.Count; i++)
            {
                Assert.Equal(results[i], resultsFlippedHorizontally[i]);
            }
        }

        [Property(Verbose = true, Arbitrary = new[] {typeof(ValidCharacters)})]
        public void ShouldFormAPoint(char letter)
        {
            var results = Diamond.Create(letter).ToList();

            var midPoint = letter.ToInt() - 1;

            var expectedNumberOfLeadingSpaces = 0;
            for (var i = midPoint; i >= 0; i--)
            {
                var row = results[i];
                var c = (i+1).ToChar();
                var actualLeadingSpaces = row.Substring(0, row.IndexOf(c));
                var reversedRow = row.Reverse().Aggregate("", (a,b) => a+b);
                var actualTrailingSpaces = reversedRow.Substring(0, reversedRow.IndexOf(c));
                
                Assert.Equal(expectedNumberOfLeadingSpaces, actualLeadingSpaces.Length);
                Assert.Equal(expectedNumberOfLeadingSpaces, actualTrailingSpaces.Length);

                expectedNumberOfLeadingSpaces++;
            }
        }

        private (char letter, int lettersPerRow, int expectedNumberOfRows) GetExpectationsForLetter(char letter)
        {
            return _expectations
                .First(x => x.Letter.ToString() == letter.ToString().ToUpper());
        }
        
        private static List<(char Letter, int LettersPerRow, int ExpectedNumberOfRows)> Init()
        {
            return Enumerable
                .Range(1, 26)
                .Select(x => ((char)(x + 64), x == 1 ? 1 : 2, x*2-1)).ToList();
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

    public static class StringExtensions
    {
        public static (string left, string right) SplitInTwo(this string row)
        {
            var left = row.Substring(0, row.Length / 2 + 1);
            var right = row.Substring(row.Length / 2).ToCharArray().Reverse().Aggregate("", (a, b) => a.ToString() + b.ToString());
            return (left, right);
        }
    }
}