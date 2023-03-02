using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public static class SyncDebug
    {
        /// <summary>
        /// Initialize a dictionary with 100 items using threads. Each item should only be initialized once.
        /// </summary>
        /// <param name="getItem"></param>
        /// <returns></returns>
        public static Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            const int capacity = 100;

            // ProcessorCount is LOGICAL processor count. AKA, physical core count * 2 to allow for multi-threading
            var processorCount = Environment.ProcessorCount;

            // Always subtract two from total processor count to allow for the operating system and demand
            if (processorCount > 2)
                processorCount -= 2;
            else
                processorCount = 1;

            var itemsToInitialize = Enumerable.Range(start: 0, count: capacity).ToList();

            // Always pre-allocate.
            var concurrentDictionary = new ConcurrentDictionary<int, string>(concurrencyLevel: processorCount, capacity: capacity);

            var threads = Enumerable.Range(start: 0, count: processorCount)
                .Select(selector: i => new Thread(start: () =>
                {
                    var remainder = capacity % processorCount;

                    var length = (int)Math.Floor(capacity / (double)processorCount);

                    var startIndex = i * length;

                    if (i == processorCount - 1)
                        length += remainder;

                    var span = new ReadOnlySpan<int>(itemsToInitialize.ToArray());

                    var itemSpan = span.Slice(start: startIndex, length: length);

                    foreach (var item in itemSpan)
                        concurrentDictionary.AddOrUpdate(key: item, addValueFactory: getItem, updateValueFactory: (_, s) => s);
                }))
                .ToList();

            foreach (var thread in threads) thread.Start();
            foreach (var thread in threads) thread.Join();

            return concurrentDictionary.ToDictionary(keySelector: kv => kv.Key, elementSelector: kv => kv.Value);
        }

        /// <summary>
        /// Initialize a ConcurrentBag using Parallel.ForEach().
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static List<string> InitializeListParallel(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();

            void Body(string i)
            {
                bag.Add(item: i);
            }

            // Default behavior is to wait for all threads to finish.
            Parallel.ForEach(source: items, body: Body);

            return bag.ToList();
        }

        /// <summary>
        /// Initialize a ConcurrentBag using asynchronous tasks.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task<List<string>> InitializeListAsync(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();

            // Must use await to ensure all tasks are completed before continuing execution.
            await Parallel.ForEachAsync(source: items, body: async (i, _) =>
            {
                var r = await Task.Run(function: () => i, cancellationToken: new CancellationToken())
                    .ConfigureAwait(continueOnCapturedContext: false);

                bag.Add(item: r);
            });

            return bag.ToList();
        }
    }
}