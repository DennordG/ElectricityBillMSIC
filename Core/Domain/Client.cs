using System;

namespace ElectricityBillMSIC.Core.Domain
{
    public class Client
    {
        public string Code { get; set; }

        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}
