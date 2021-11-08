using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDictionary<string, Client> _clientsByCode = new ConcurrentDictionary<string, Client>(StringComparer.OrdinalIgnoreCase);

        public bool TryAdd(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.Code))
            {
                return false;
            }

            return _clientsByCode.TryAdd(client.Code, client);
        }

        public Client Get(string clientCode)
        {
            if (_clientsByCode.TryGetValue(clientCode, out var client))
            {
                return client;
            }

            return null;
        }

        public ICollection<Client> GetAll()
        {
            return _clientsByCode.Values;
        }
    }
}
