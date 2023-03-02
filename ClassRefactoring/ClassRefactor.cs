using System;

namespace DeveloperSample.ClassRefactoring
{
    public enum SparrowType
    {
        African, European
    }

    public enum SparrowLoad
    {
        None, Coconut
    }

    /// <summary>
    /// Creates a new Sparrow.
    /// </summary>
    /// <remarks>
    /// CA1822: Mark members as static
    /// </remarks>>
    public static class SparrowFactory
    {
        public static Sparrow GetSparrow(SparrowType sparrowType) =>
            new(sparrowType: sparrowType);
    }

    public class Sparrow
    {
        public SparrowType Type { get; }

        public SparrowLoad Load { get; private set; }

        public Sparrow(SparrowType sparrowType) => Type = sparrowType;

        public void ApplyLoad(SparrowLoad load) => Load = load;

        public double GetAirspeedVelocity() =>
            Type switch
            {
                // African
                SparrowType.African when Load == SparrowLoad.None => 22,
                SparrowType.African when Load == SparrowLoad.Coconut => 18,

                // European
                SparrowType.European when Load == SparrowLoad.None => 20,
                SparrowType.European when Load == SparrowLoad.Coconut => 16,
                _ => throw new InvalidOperationException()
            };
    }
}