using System;
using System.Collections.Generic;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Core.Repositories
{
    internal class ClientDictionaryRepository : AbstractDictionaryRepository<string, Client>, IClientRepository
    {
        protected override IEqualityComparer<string> KeyComparer => StringComparer.OrdinalIgnoreCase;

        public bool TryAdd(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.Code))
            {
                return false;
            }

            return TryAdd(client, c => c.Code);
        }
    }
}
