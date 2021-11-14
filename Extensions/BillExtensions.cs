using System.Collections.Generic;
using System.IO;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Extensions
{
    public static class BillExtensions
    {
        public static void Print(this Bill bill, TextWriter writer)
        {
            writer.WriteLine(bill.DateOfIssue.ToString("dd/MM/yyyy"));
            writer.WriteLine(bill.IsPaid ? "PAID" : "NOT PAID");
            writer.WriteLine("Consumation (KW): {0}", bill.ConsumationInKw.ToString("N2"));
            writer.WriteLine("Cost: {0}", bill.Cost.ToString("C"));
            writer.WriteLine("-------------------------------------------------");
        }

        public static void Print(this IEnumerable<Bill> bills, TextWriter writer)
        {
            bills.Do(c => c.Print(writer));
        }
    }
}
