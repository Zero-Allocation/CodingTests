using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace DeveloperSample.Syncing
{
    public class SyncTest
    {
        [Fact]
        public void CanInitializeCollectionParallel()
        {
            // CA1822
            var items = new List<string> { "one", "two" };
            var result = SyncDebug.InitializeListParallel(items: items);
            Assert.Equal(expected: items.Count, actual: result.Count);
        }

        [Fact]
        public void CanInitializeCollectionTpl()
        {
            // CA1822
            var items = new List<string> { "one", "two" };

            // Using .Result forces execution to wait until the method finishes.
            var result = SyncDebug.InitializeListAsync(items: items).Result;

            Assert.Equal(expected: items.Count, actual: result.Count);
        }

        [Fact]
        public void ItemsOnlyInitializeOnce()
        {
            // CA1822
            var count = 0;
            var dictionary = SyncDebug.InitializeDictionary(getItem: i =>
            {
                Thread.Sleep(millisecondsTimeout: 1);
                Interlocked.Increment(location: ref count);
                return i.ToString();
            });

            Assert.Equal(expected: 100, actual: count);
            Assert.Equal(expected: 100, actual: dictionary.Count);
        }
    }
}