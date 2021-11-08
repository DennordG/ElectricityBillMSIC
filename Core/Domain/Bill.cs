namespace ElectricityBillMSIC.Core.Domain
{
    public class Bill
    {
        public string ClientCode { get; set; }

        public int ConsumationInKw { get; set; }
        public double Cost { get; set; }
    }
}
