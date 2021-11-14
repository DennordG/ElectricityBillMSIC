using System;
using System.Collections.Generic;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    public interface IBillRepository : IRepository<Guid, Bill>
    {
        IEnumerable<Bill> GetAll(string clientCode);
    }
}
