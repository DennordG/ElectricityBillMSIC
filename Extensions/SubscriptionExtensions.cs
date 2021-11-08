using System.Collections.Generic;
using System.IO;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Extensions
{
    public static class SubscriptionExtensions
    {
        public static void Print(this Subscription subscription, TextWriter writer)
        {
            writer.WriteLine(subscription.Id);
            writer.WriteLine(subscription.SubscriptionType);
            writer.WriteLine("-------------------------------------------------");
        }

        public static void Print(this IEnumerable<Subscription> subscriptions, TextWriter writer)
        {
            subscriptions.Do(s => s.Print(writer));
        }
    }
}
