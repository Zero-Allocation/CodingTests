using Xunit;

namespace DeveloperSample.ClassRefactoring
{
    public class ClassRefactorTest
    {
        [Fact]
        public void AfricanSparrowHasCorrectSpeed()
        {
            var sparrow = SparrowFactory.GetSparrow(sparrowType: SparrowType.African);
            Assert.Equal(expected: 22, actual: sparrow.GetAirspeedVelocity());
        }

        [Fact]
        public void LadenAfricanSparrowHasCorrectSpeed()
        {
            var sparrow = SparrowFactory.GetSparrow(sparrowType: SparrowType.African);
            sparrow.ApplyLoad(load: SparrowLoad.Coconut);
            Assert.Equal(expected: 18, actual: sparrow.GetAirspeedVelocity());
        }

        [Fact]
        public void EuropeanSparrowHasCorrectSpeed()
        {
            var sparrow = SparrowFactory.GetSparrow(sparrowType: SparrowType.European);
            Assert.Equal(expected: 20, actual: sparrow.GetAirspeedVelocity());
        }

        [Fact]
        public void LadenEuropeanSparrowHasCorrectSpeed()
        {
            var sparrow = SparrowFactory.GetSparrow(sparrowType: SparrowType.European);
            sparrow.ApplyLoad(load: SparrowLoad.Coconut);
            Assert.Equal(expected: 16, actual: sparrow.GetAirspeedVelocity());
        }
    }
}