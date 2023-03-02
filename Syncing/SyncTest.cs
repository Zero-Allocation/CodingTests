using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace DeveloperSample.Syncing
{
    public class SyncTest
    {
        [Fact]
        public void CanInitializeCollection()
        {
            var debug = new SyncDebug();
            var items = new List<string> { "one", "two" };
            var result = debug.InitializeList(items: items);
            Assert.Equal(expected: items.Count, actual: result.Count);
        }

        [Fact(Skip = "Not implemented")]
        public void ItemsOnlyInitializeOnce()
        {
            var debug = new SyncDebug();
            var count = 0;
            var dictionary = debug.InitializeDictionary(getItem: i =>
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