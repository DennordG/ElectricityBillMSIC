using System;
using System.Collections.Generic;

namespace ElectricityBillMSIC.Core.Repositories
{
    public interface IDictionaryRepository<TKey, TValue>
    {
        bool TryAdd(TValue value, Func<TValue, TKey> keySelector);
        TValue Get(TKey key);
        ICollection<TValue> GetAll();
    }
}
