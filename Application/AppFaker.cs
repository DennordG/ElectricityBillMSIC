using System;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Application
{
    internal static class AppFaker
    {
        private enum KindOfFakeBill
        {
            UnpaidOld,
            UnpaidRecent,
            Paid
        }

        private static readonly Randomizer Randomizer = new Randomizer();

        private static readonly DateTime StartOfCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);

        private static string GenerateClientCode(Faker f) => f.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

        private static readonly Faker<Client> ClientFaker = new Faker<Client>()
                .StrictMode(true)
                .RuleFor(c => c.Code, GenerateClientCode)
                .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName((Name.Gender)c.Gender))
                .RuleFor(c => c.LastName, (f, c) => f.Name.LastName((Name.Gender)c.Gender))
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.SubscriptionId, f => f.Random.Guid());

        private static readonly Faker<Subscription> SubscriptionFaker = new Faker<Subscription>()
                .StrictMode(true)
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.SubscriptionType, f => f.PickRandom<SubscriptionType>());

        private static readonly Faker<Bill> BillFaker = new Faker<Bill>()
                .StrictMode(true)
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.ClientCode, GenerateClientCode)
                .RuleFor(c => c.IsPaid, f => f.Random.Bool())
                .RuleFor(c => c.ConsumationInKw, f => f.Random.Double(0.0, 1000.0))
                .RuleFor(c => c.Cost, f => f.Random.Double(40.0, 200.0))
                .RuleFor(c => c.DateOfIssue, f => f.Date.Between(StartOfCurrentYear, DateTime.Today));

        public static Client GenerateClient(Guid subscriptionId) => ClientFaker.RuleFor(c => c.SubscriptionId, subscriptionId).Generate();

        public static Subscription GenerateSubscription() => SubscriptionFaker.Generate();

        public static Bill GenerateBill(string clientCode) => BillFaker.RuleFor(b => b.ClientCode, clientCode).Generate();

        public static Bill GenerateUnpaidRecentBill(string clientCode) => BillFaker
            .RuleFor(b => b.ClientCode, clientCode)
            .RuleFor(b => b.IsPaid, false)
            .RuleFor(b => b.DateOfIssue, f => f.Date.Recent())
            .Generate();

        public static Bill GenerateUnpaidOldBill(string clientCode) => BillFaker
            .RuleFor(b => b.ClientCode, clientCode)
            .RuleFor(b => b.IsPaid, false)
            .RuleFor(b => b.DateOfIssue, f => f.Date.Past())
            .Generate();

        public static Bill GeneratePaidBill(string clientCode) => BillFaker
            .RuleFor(b => b.ClientCode, clientCode)
            .RuleFor(b => b.IsPaid, true)
            .Generate();

        public static IEnumerable<Bill> GenerateBills(string clientCode, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var kindOfFakeBill = Randomizer.Enum<KindOfFakeBill>();
                switch (kindOfFakeBill)
                {
                    case KindOfFakeBill.UnpaidOld:
                        yield return GenerateUnpaidOldBill(clientCode);
                        break;
                    case KindOfFakeBill.UnpaidRecent:
                        yield return GenerateUnpaidRecentBill(clientCode);
                        break;
                    case KindOfFakeBill.Paid:
                        yield return GeneratePaidBill(clientCode);
                        break;
                    default:
                        throw new InvalidOperationException($"Kind of fake bill not handled in the generation: {kindOfFakeBill}.");
                }
            }
        }
    }
}
