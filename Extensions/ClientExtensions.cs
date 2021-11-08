using System.Collections.Generic;
using System.IO;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Extensions
{
    public static class ClientExtensions
    {
        public static void Print(this Client client, TextWriter writer)
        {
            writer.WriteLine(client.Code);
            writer.WriteLine("{0} {1}", client.FirstName, client.LastName);
            writer.WriteLine(client.Address);
            writer.WriteLine(client.SubscriptionId);
            writer.WriteLine("-------------------------------------------------");
        }

        public static void Print(this IEnumerable<Client> clients, TextWriter writer)
        {
            clients.Do(c => c.Print(writer));
        }
    }
}
