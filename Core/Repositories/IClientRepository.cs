using System.Collections.Generic;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    public interface IClientRepository
    {
        bool TryAdd(Client client);
        Client Get(string clientCode);
        ICollection<Client> GetAll();
    }
}
