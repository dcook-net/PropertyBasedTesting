using System;

namespace Calculator
{
    public static class CalcV1
    {
        public static dynamic Add(string a, string b)
        {
            if (a is null || b is null) return 0;

            var x = Convert.ToInt32(a);
            var y = Convert.ToInt32(b);
            
            return x + y;
        }
    }
}