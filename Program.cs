using System;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using ElectricityBillMSIC.Core;

namespace ElectricityBillMSIC
{
    class Program
    {
        static void Main(string[] args)
        {
            const int clientsCount = 5;

            var subscriptionFaker = new Faker<Subscription>()
                .StrictMode(true)
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.SubscriptionType, f => f.PickRandom<SubscriptionType>());

            var subscriptions = new List<Subscription>();

            var clientFaker = new Faker<Client>()
                .StrictMode(true)
                .RuleFor(c => c.Code, f => f.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"))
                .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName((Name.Gender)c.Gender))
                .RuleFor(c => c.LastName, (f, c) => f.Name.LastName((Name.Gender)c.Gender))
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.SubscriptionId, f => f.Random.Guid())
                .FinishWith((f, c) =>
                {
                    var clientSubscription = subscriptionFaker.Generate();
                    clientSubscription.Id = c.SubscriptionId;

                    subscriptions.Add(clientSubscription);
                });

            var clients = clientFaker.Generate(clientsCount);

            Console.WriteLine("Clients:");
            foreach (var client in clients)
            {
                Console.WriteLine(client.Code);
                Console.WriteLine("{0} {1}", client.FirstName, client.LastName);
                Console.WriteLine(client.Address);
                Console.WriteLine(client.SubscriptionId);
                Console.WriteLine("-------------------------------------------------");
            }

            Console.WriteLine("#################################################");

            Console.WriteLine("Subscriptions:");
            foreach (var subscription in subscriptions)
            {
                Console.WriteLine(subscription.Id);
                Console.WriteLine(subscription.SubscriptionType);
                Console.WriteLine("-------------------------------------------------");
            }
        }
    }
}
