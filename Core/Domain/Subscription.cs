using System;

namespace ElectricityBillMSIC.Core.Domain
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
