using System;

namespace ElectricityBillMSIC.Core
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
