using System;
using System.Collections.Generic;
using System.Linq;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    internal class BillDictionaryRepository : AbstractDictionaryRepository<Guid, Bill>, IBillRepository
    {
        protected override IEqualityComparer<Guid> KeyComparer => EqualityComparer<Guid>.Default;

        public IEnumerable<Bill> GetAll(string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                return Enumerable.Empty<Bill>();
            }

            return GetAll().Where(b => string.Equals(b.ClientCode, clientCode, StringComparison.OrdinalIgnoreCase));
        }

        public bool TryAdd(Bill value)
        {
            if (string.IsNullOrEmpty(value.ClientCode))
            {
                return false;
            }

            return TryAdd(value, b => b.Id);
        }
    }
}
