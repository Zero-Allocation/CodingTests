using Xunit;

namespace DeveloperSample.Algorithms
{
    public class AlgorithmTest
    {
        [Fact]
        public void CanFormatSeparators() => Assert.Equal(expected: "a, b and c", actual: Algorithms.FormatSeparators(
            items: new[] { "a", "b", "c" }));

        [Fact]
        public void CanGetBigFactorial() => Assert.Equal(expected: 24, actual: Algorithms.GetBigFactorial(n: 4));

        [Fact]
        public void CanGetFactorial() => Assert.Equal(expected: 24, actual: Algorithms.GetFactorial(n: 4));
    }
}