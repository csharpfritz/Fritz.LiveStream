using System;
using System.Buffers.Text;
using System.Text;

namespace TwitchySpan
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample_SummingWithSpan.Run();
            Sample_ParsingWithSpan.Run();
            Sample_StackAllocation.Run();
            Sample_Utf8.Run();
        }
    }

    static class Sample_SummingWithSpan
    {
        public static void Run()
        {
            int[] data = new[] { 1, 2, 3 };
            int sum = Sum(data);
            Console.WriteLine(sum);
        }

        // Using an arry is old-school. With span, you can just change the signature
        // to Span<int> and both the body as well as the consuming code will largely
        // just compile again.
        //
        //static int Sum(int[] numbers)
        //{
        //    int result = 0;
        //    for (int i = 0; i < numbers.Length; i++)
        //    {
        //        result += numbers[i];
        //    }
        //    return result;
        //}

        static int Sum(Span<int> numbers)
        {
            int result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                result += numbers[i];
            }
            return result;
        }
    }

    static class Sample_ParsingWithSpan
    {
        public static void Run()
        {
            string data = "42,10";
            int sum = ParseSum(data);
            Console.WriteLine(sum);
        }

        // The problem with the code below is that it allocates quite a bit:
        //
        // 1. It creates an array to represent the parts separated by commas.
        // 2. It creates a string per part just so we can parse the int.
        //
        // We could rewrite the parsing logic using a loop, which would allow
        // us to get rid of the array allocation but in order to parse the int
        // we still need to create the string in order to call int.Parse(string).
        // But with our new int.Parse(ReadOnlySpan<char>) method we can parse
        // the int without having to create a string first.
        //
        //static int ParseSum(string data)
        //{
        //    int sum = 0;
        //    string[] splitString = data.Split(',');
        //    for (int i = 0; i < splitString.Length; i++)
        //    {
        //        sum += int.Parse(splitString[i]);
        //    }
        //    return sum;
        //}

        static int ParseSum(ReadOnlySpan<char> span)
        {
            int sum = 0;
            while (true)
            {
                // Find next comma

                int index = span.IndexOf(',');
                if (index == -1)
                {
                    sum += int.Parse(span);
                    break;
                }

                // Parse text

                ReadOnlySpan<char> numberText = span.Slice(0, index);
                sum += int.Parse(numberText);

                // Skip comma
                span = span.Slice(index + 1);

            }
            return sum;
        }
    }

    static class Sample_StackAllocation
    {
        public static void Run()
        {
            int result = Sum(1, 2, 3);
            Console.WriteLine(result);
        }

        // Let's say we want to provide convenience helpers that can sum a specific
        // number of ints that are directly passed as arguments. Normally we'd be using
        // the params modifier in C# but this requires an array to be created.
        // 
        // With spans, we can just use the stackalloc feature that exists in C# since V1
        // but was previously only availavble to unsafe code (meaning it requires
        // pointers).
        // 
        // With span, you can now use stackalloc to create a span that lives on the stack
        // (as opposed to the heap). This makes this creation basically free.
        //
        //static int Sum(int n1, int n2, int n3)
        //{
        //    return Sum(new[] { n1, n2, n3 });
        //}

        static int Sum(int n1, int n2, int n3)
        {
            Span<int> span = stackalloc int[3];
            span[0] = n1;
            span[1] = n2;
            span[2] = n3;
            return Sum(span);
        }

        static int Sum(Span<int> numbers)
        {
            var result = 0;
            for (var i = 0; i < numbers.Length; i++)
            {
                result += numbers[i];
            }
            return result;
        }
    }

    static class Sample_Utf8
    {
        public static void Run()
        {
            // Let's pretend we got the data in form of a UTF8
            // encoded data (this could be an array or span).
            byte[] data = Encoding.UTF8.GetBytes("42,10");

            int result = ParseFromUtf8(data);
            Console.WriteLine(result);
        }

        // Normally, C# developers have to convert UTF8 to UTF16 in order
        // to process string data. This requires an allocation of a new
        // string.
        //
        // With spans, we've added new APIs that can be used to parse data
        // directly from UTF8 encoded data.
        // 
        //private static int ParseFromUtf8(byte[] utf8)
        //{
        //    string text = Encoding.UTF8.GetString(utf8);
        //    return ParseFromUtf16(text);
        //}
        //
        //private static int ParseFromUtf16(ReadOnlySpan<char> span)
        //{
        //    int sum = 0;
        //    while (true)
        //    {
        //        int indexOfComma = span.IndexOf(',');
        //        if (indexOfComma < 0)
        //        {
        //            sum += int.Parse(span);
        //            break;
        //        }
        //
        //        int value = int.Parse(span.Slice(0, indexOfComma));
        //        sum += value;
        //
        //        span = span.Slice(indexOfComma + 1);
        //    }
        //    return sum;
        //}

        private static int ParseFromUtf8(Span<byte> utf8)
        {
            int sum = 0;
            while (true)
            {
                Utf8Parser.TryParse(utf8, out int value, out int bytesConsumed);
                sum += value;
                if (utf8.Length - 1 < bytesConsumed)
                    break;
                // skip ','
                utf8 = utf8.Slice(bytesConsumed + 1);
            }
            return sum;
        }
    }
}
