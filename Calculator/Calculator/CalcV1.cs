using System;

namespace Calculator
{
    public static class CalcV1
    {
        public static int Add(this int a, int b) =>
            Add(a.ToString(), b.ToString());
        
        //Using dynamic here only in order to simulate dynamic typing in
        //javascript. This is not an endorsement.
        //Please you the type system
        public static dynamic Add(this string a, string b)
        {
            if (a is null || b is null) return 0;

            var x = Convert.ToInt32(a);
            var y = Convert.ToInt32(b);
            
            return x + y;
        }
    }
}