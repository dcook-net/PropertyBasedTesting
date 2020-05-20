using System;
using NUnit.Framework;

using static Calculator.CalcV2;

namespace PropertyBasedTesting
{
   public class ManuallyCreatedPropertyBasedTests
   {
      [Test]
      public void Order_Of_Params_Does_Not_Matter()
      {
         for (var i = 0; i < 100; i++)
         {
            var rand = new Random();
            var a = rand.Next();
            var b = rand.Next();

            var result = a.Add(b);

            var expected = b.Add(a);

            Assert.That(result, Is.EqualTo(expected));
         }
      }

      [Test]
      public void Adding_1_Twice_Is_The_Same_As_Adding_2_Once()
      {
         for (var i = 0; i < 100; i++)
         {
            var rand = new Random();
            var x = rand.Next();

            var result = x.Add(1).Add(1);

            var expected = x.Add(2);

            Assert.That(result, Is.EqualTo(expected));
         }
      }

      [Test]
      public void Adding_Zero_To_X_Returns_X()
      {
         for (var i = 0; i < 100; i++)
         {
            var rand = new Random();
            var x = rand.Next();

            var result = x.Add(0);

            Assert.That(result, Is.EqualTo(x));
         }
      }
   }
}