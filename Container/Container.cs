using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperSample.Container
{
    /// <summary>
    /// A simple dependency injection container
    /// </summary>
    /// <remarks>
    /// I have never implemented my own DI container, so I found a tutorial
    /// online, altered it a little, and here is what we have.
    /// </remarks>
    /// <see cref="http://www.encora.com/insights/writing-a-minimal-ioc-container-in-c"/>
    public class Container
    {
        private readonly Dictionary<Type, Type> _types = new();

        public void Bind(Type interfaceType, Type implementationType) =>
            _types[interfaceType] = implementationType;

        public T Get<T>() =>
            (T)Create(typeof(T));

        private object Create(Type type)
        {
            // Find a default constructor using reflection
            var concreteType = _types[type];
            var defaultConstructor = concreteType.GetConstructors()[0];

            // Verify if the default constructor requires params
            var defaultParams = defaultConstructor.GetParameters();

            // Instantiate all constructor parameters using recursion
            var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();

            return defaultConstructor.Invoke(parameters);
        }
    }
}