using static Calculator.CalcV2;
using FsCheck.Xunit;
using Xunit;

namespace PropertyBasedTesting
{
    public class PropertyBasedTestsWithFsCheck
    {
        [Property]
        public void Order_Of_Params_Does_Not_Matter(int a, int b)
        {
            var result = a.Add(b);
    
            var expected = b.Add(a);
    
            Assert.Equal(result, expected);
        }
        
        [Property]
        public void Adding_1_Twice_Is_The_Same_As_Adding_2_Once(int x)
        {
            var result = x.Add(1).Add(1);
    
            var expected = x.Add(2);
    
            Assert.Equal(result, expected);
        }

        [Property]
        public void Adding_Zero_To_X_Returns_X(int x)
        {
            var result = x.Add(0);

            Assert.Equal(result, x);
        }
    }
}