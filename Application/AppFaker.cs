using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using ElectricityBillMSIC.Core.Domain;

namespace ElectricityBillMSIC.Application
{
    internal static class AppFaker
    {
        private static readonly Faker<Subscription> SubscriptionFaker = new Faker<Subscription>()
                .StrictMode(true)
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.SubscriptionType, f => f.PickRandom<SubscriptionType>());

        private static readonly Faker<Client> ClientFaker = new Faker<Client>()
                .StrictMode(true)
                .RuleFor(c => c.Code, f => f.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"))
                .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName((Name.Gender)c.Gender))
                .RuleFor(c => c.LastName, (f, c) => f.Name.LastName((Name.Gender)c.Gender))
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.SubscriptionId, f => f.Random.Guid());

        public static Subscription GenerateSubscription() => SubscriptionFaker.Generate();
        public static IEnumerable<Subscription> GenerateSubscriptions() => SubscriptionFaker.GenerateForever();

        public static Client GenerateClient() => ClientFaker.Generate();
        public static IEnumerable<Client> GenerateClients() => ClientFaker.GenerateForever();
    }
}
