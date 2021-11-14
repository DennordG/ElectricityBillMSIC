using System;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    public interface ISubscriptionRepository : IRepository<Guid, Subscription>
    { }
}
