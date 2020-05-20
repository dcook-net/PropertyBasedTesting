using NUnit.Framework;
using static Calculator.CalcV1;

namespace ExampleBasedTesting
{
    public class Tests
    {
        /// Following TDD principles,
        /// First we create a failing test, then write enough code to make that test pass
        /// Then another
        /// Then another
        [Test]
        public void ShouldAddTwoNumbersTogether()
        {
            const int a = 10;
            const int b = 2;
            const int expected = 12;

            var result = Add(a.ToString(), b.ToString());

            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void ShouldAddTwoNegativeNumbersTogether()
        {
            const int a = -10;
            const int b = -2;
            const int expected = -12;

            var result = Add(a.ToString(), b.ToString());

            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void ShouldAddTwoZerosTogether()
        {
            const int zero = 0;
            
            var result = Add(zero.ToString(), zero.ToString());
        
            Assert.That(result, Is.EqualTo(zero));
        }
    }
}