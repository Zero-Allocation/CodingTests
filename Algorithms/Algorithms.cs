using Cysharp.Text;
using System;
using System.Numerics;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        /// <summary>
        /// <para>Format a given array of strings with commas and a final 'and' separator.</para>
        /// <para>["a", "b", "c"] => "a, b and c"</para>
        /// </summary>
        /// <remarks>
        /// Benchmark this against other implementations to determine performance efficacy of span.
        /// </remarks>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string FormatSeparators(params string[] items)
        {
            ReadOnlySpan<string> spanItems = items;
            var spanCommaSeparatedItems = spanItems[..(items.Length - 1)];

            using var zBuilder = ZString.CreateStringBuilder();
            zBuilder.AppendJoin(separator: ", ", values: spanCommaSeparatedItems);
            zBuilder.Append(value: " and ");
            zBuilder.Append(value: items[^1]);

            return zBuilder.ToString();
        }

        /// <summary>
        /// Tail Recursion in C#.
        /// </summary>
        /// <remarks>
        /// BigInteger operations are SLOW.
        /// Look into using CORDIC and logarithms for factorials.
        /// </remarks>
        /// <param name="n"></param>
        /// <returns></returns>
        public static BigInteger GetBigFactorial(int n)
        {
            var fact = Trampoline.MakeTrampoline<int, BigInteger, BigInteger>(function: Bounce.Factorial);

            return fact(arg1: n, arg2: 1);
        }

        /// <summary>
        /// Use recursion to calculate a factorial.
        /// </summary>
        /// <remarks>
        /// This will throw a stack overflow exception with sufficiently large numbers.
        /// </remarks>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int GetFactorial(int n)
        {
            if (n == 1)
                return 1;

            return n * GetFactorial(n: n - 1);
        }
    }
}