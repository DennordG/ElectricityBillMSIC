using System;
using System.Collections.Generic;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    internal class SubscriptionDictionaryRepository : AbstractDictionaryRepository<Guid, Subscription>, ISubscriptionRepository
    {
        protected override IEqualityComparer<Guid> KeyComparer => EqualityComparer<Guid>.Default;

        public bool TryAdd(Subscription value)
        {
            return TryAdd(value, s => s.Id);
        }
    }
}
