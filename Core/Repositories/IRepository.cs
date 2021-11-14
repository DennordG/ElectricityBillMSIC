using System.Collections.Generic;

namespace ElectricityBillMSIC.Core.Repositories
{
    public interface IRepository<TId, TValue>
    {
        bool TryAdd(TValue value);
        TValue Get(TId id);
        ICollection<TValue> GetAll();
    }
}
