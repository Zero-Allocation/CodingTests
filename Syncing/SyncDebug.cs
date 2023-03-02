using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            Parallel.ForEach(source: items, body: async i =>
            {
                var r = await Task.Run(function: () => i).ConfigureAwait(continueOnCapturedContext: false);
                bag.Add(item: r);
            });
            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(start: 0, count: 100).ToList();

            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var threads = Enumerable.Range(start: 0, count: 3)
                .Select(selector: i => new Thread(start: () =>
                {
                    foreach (var item in itemsToInitialize) concurrentDictionary.AddOrUpdate(key: item, addValueFactory: getItem, updateValueFactory: (_, s) => s);
                }))
                .ToList();

            foreach (var thread in threads) thread.Start();
            foreach (var thread in threads) thread.Join();

            return concurrentDictionary.ToDictionary(keySelector: kv => kv.Key, elementSelector: kv => kv.Value);
        }
    }
}