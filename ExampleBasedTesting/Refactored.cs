using NUnit.Framework;
using static Calculator.CalcV1;

namespace ExampleBasedTesting
{
    public class Refactored
    {
        /// As our tests have gone green,
        /// We've refactored our test code to reduce duplication 
        [TestCase("10", "2", 12)]
        [TestCase("-10", "-2", -12)]
        [TestCase("10", "-2", 8)]
        [TestCase("-10", "2", -8)]
        [TestCase("0", "0", 0)]
        [TestCase("1", "2", 3)]
        [TestCase(null, "2", 0)]
        [TestCase("2", null, 0)]
        [TestCase(null, null, 0)]
        [TestCase("1", "2147483647", -2147483648)]
        //Using dynamic here only in order to simulate dynamic typing in
        //javascript. This is not an endorsement.
        //Please you the type system
        public void ShouldAddTwoNumberTogether(string a, string b, dynamic expected)
        {
            var result = a.Add(b);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}