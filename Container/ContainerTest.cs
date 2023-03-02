using System;
using Xunit;

namespace DeveloperSample.Container
{
    internal interface IContainerTestInterface
    {
        // ReSharper disable once UnusedMember.Global
        // We need it to have an interface to test
        bool WillItFloat(string maybe);
    }

    internal class ContainerTestClass : IContainerTestInterface
    {
        private readonly Random _random = new();

        public bool WillItFloat(string maybe) =>
            Math.Round(_random.NextDouble()) != 0;
    }

    public class ContainerTest
    {
        [Fact]
        public void CanBindAndGetService()
        {
            var container = new Container();
            container.Bind(interfaceType: typeof(IContainerTestInterface), implementationType: typeof(ContainerTestClass));

            var testInstance = container.Get<IContainerTestInterface>();
            Assert.IsType<ContainerTestClass>(@object: testInstance);
        }
    }
}