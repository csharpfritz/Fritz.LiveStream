using System;

using static System.Console;

namespace CSharp7Features
{

    class Program
    {

        static void Main(string[] args)
        {
            object[] numbers = { 0b1, "42", 0b10, null, 0b100, new object[] { 0b_1000, 0b1_0000 }, 0b10_0000 };

            var (sum, _) = Tally(numbers);

            WriteLine($"Sum: {sum}");
        }

        static (int sum, int count) Tally(object[] values)
        {
            var r = (s: 0, c: 0);
            foreach (var v in values)
            {
                switch (v)
                {
                    case int i:
                        Add(i, 1);
                        break;
                    case object[] a when a.Length > 0:
                        var (s, c) = Tally(a);
                        Add(s, c);
                        break;
                    case object[] _:
                    case null:
                        break;
                    case string str when int.TryParse(str, out var i):
                        Add(i, 1);
                        break;
                }
            }
            return r;

            void Add(int s, int c) => r = (r.s + s, r.c + c);
        }
        
    }

}
