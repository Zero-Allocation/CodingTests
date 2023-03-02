using System.Numerics;

namespace DeveloperSample.Algorithms
{
    public static class Bounce
    {
        public static Bounce<int, BigInteger, BigInteger> Factorial(int n, BigInteger product) =>
            n < 2 ?
                Trampoline.ReturnResult<int, BigInteger, BigInteger>(product) :
                Trampoline.Recurse<int, BigInteger, BigInteger>(n - 1, n * product);
    }
}