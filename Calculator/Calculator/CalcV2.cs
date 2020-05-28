namespace Calculator
{
    public static class CalcV2
    {
        public static int Add(this int a, int b) =>
            Add(a.ToString(), b.ToString());

        //Using dynamic here only in order to simulate dynamic typing in
        //javascript. This is not an endorsement.
        //Please you the type system
        private static dynamic Add(string a, string b)
        {
            if (a is null || b is null) return 0;

            if (int.TryParse(a, out var x) && int.TryParse(b, out var y))
                return x + y;

            return 0;
        }
    }
}