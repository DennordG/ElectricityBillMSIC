using System;

namespace ElectricityBillMSIC.Core.Domain
{
    public class Bill
    {
        public Guid Id { get; set; }
        public string ClientCode { get; set; }
        public DateTime DateOfIssue { get; set; }
        public bool IsPaid { get; set; }

        public double ConsumationInKw { get; set; }
        public double Cost { get; set; }
    }
}
