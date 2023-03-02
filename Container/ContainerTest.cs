using Xunit;

namespace DeveloperSample.Container
{
    internal interface IContainerTestInterface
    {
    }

    internal class ContainerTestClass : IContainerTestInterface
    {
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