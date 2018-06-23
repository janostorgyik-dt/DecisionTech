using System.Collections.Concurrent;

namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Stores data in memory.
    /// Helper class and not considered as part of the solution... (no unit test, no type check, etc...)
    /// </summary>
    public class InMemoryStorageService : IStorageService
    {
        private readonly ConcurrentDictionary<string, object> _store = new ConcurrentDictionary<string, object>();

        public void Put<T>(string key, T value) where T : class
        {
            _store.AddOrUpdate(key, value, (k, old) => value);
        }

        public T Get<T>(string key) where T : class
        {
            if (this._store.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return null;
        }
    }
}