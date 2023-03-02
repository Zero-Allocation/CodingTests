using System;

namespace DeveloperSample.Algorithms
{
    /// <summary>
    /// Tail Recursion in C#.
    /// </summary>
    /// <seealso cref="http://thomaslevesque.com/2011/09/02/tail-recursion-in-c/"/>
    /// <seealso cref="http://blog.functionalfun.net/2008/04/bouncing-on-your-tail.html"/>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public readonly struct Bounce<T1, T2, TResult>
    {
        public Bounce(T1 param1, T2 param2) : this()
        {
            Param1 = param1;
            Param2 = param2;
            HasResult = false;
            Recurse = true;
        }

        public Bounce(TResult result) : this()
        {
            Result = result;
            HasResult = true;
            Recurse = false;
        }

        public bool HasResult { get; }
        public T1 Param1 { get; }
        public T2 Param2 { get; }
        public bool Recurse { get; }
        public TResult Result { get; }
    }

    /// <summary>
    /// Tail Recursion in C#.
    /// </summary>
    /// <see cref="http://thomaslevesque.com/2011/09/02/tail-recursion-in-c/"/>
    /// <see cref="http://blog.functionalfun.net/2008/04/bouncing-on-your-tail.html"/>
    public static class Trampoline
    {
        public static Func<T1, T2, TResult> MakeTrampoline<T1, T2, TResult>(Func<T1, T2, Bounce<T1, T2, TResult>> function)
        {
            TResult Trampolined(T1 arg1, T2 arg2)
            {
                var currentArg1 = arg1;
                var currentArg2 = arg2;

                while (true)
                {
                    var result = function(arg1: currentArg1, arg2: currentArg2);

                    if (result.HasResult) return result.Result;

                    currentArg1 = result.Param1;
                    currentArg2 = result.Param2;
                }
            }

            return Trampolined;
        }

        public static Bounce<T1, T2, TResult> Recurse<T1, T2, TResult>(T1 arg1, T2 arg2) => new(param1: arg1, param2: arg2);

        public static Bounce<T1, T2, TResult> ReturnResult<T1, T2, TResult>(TResult result) => new(result: result);
    }
}