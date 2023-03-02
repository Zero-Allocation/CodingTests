using System.Numerics;

namespace DeveloperSample.Algorithms
{
    public static class Bounce
    {
        public static Bounce<int, BigInteger, BigInteger> Factorial(int n, BigInteger product) =>
            n < 2 ?
                Trampoline.ReturnResult<int, BigInteger, BigInteger>(result: product) :
                Trampoline.Recurse<int, BigInteger, BigInteger>(arg1: n - 1, arg2: n * product);
    }
}