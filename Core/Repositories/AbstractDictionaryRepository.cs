using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ElectricityBillMSIC.Core.Repositories
{
    internal abstract class AbstractDictionaryRepository<TKey, TValue> : IDictionaryRepository<TKey, TValue>
    {
        protected readonly IDictionary<TKey, TValue> Dictionary;

        protected AbstractDictionaryRepository()
        {
            Dictionary = new ConcurrentDictionary<TKey, TValue>(KeyComparer);
        }

        protected abstract IEqualityComparer<TKey> KeyComparer { get; }

        public bool TryAdd(TValue value, Func<TValue, TKey> keySelector)
        {
            return Dictionary.TryAdd(keySelector(value), value);
        }

        public TValue Get(TKey key)
        {
            if (Dictionary.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public ICollection<TValue> GetAll()
        {
            return Dictionary.Values;
        }
    }
}
