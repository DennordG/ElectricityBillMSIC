using System;
using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Domain;
using ElectricityBillMSIC.Core.Repositories;

namespace ElectricityBillMSIC.Application
{
    internal class AddNewClientDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IBillRepository _billRepository;

        public AddNewClientDecisionHandler(IClientRepository clientRepository, ISubscriptionRepository subscriptionRepository, IBillRepository billRepository)
        {
            _clientRepository = clientRepository;
            _subscriptionRepository = subscriptionRepository;
            _billRepository = billRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.AddNewClient;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            var subscription = AppFaker.GenerateSubscription();
            var client = AppFaker.GenerateClient(subscription.Id);

            parameter.Writer.WriteLine();

            if (_subscriptionRepository.TryAdd(subscription) && _clientRepository.TryAdd(client))
            {
                parameter.Writer.WriteLine("Generated new client with Code: {0}.", client.Code);

                GenerateClientBills(client);

                parameter.Writer.WriteLine("Generated bills for the new client.");
            }
            else
            {
                parameter.Writer.WriteLine("Failed to generate a new client.");
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }

        private void GenerateClientBills(Client client)
        {
            foreach (var bill in AppFaker.GenerateBills(client.Code, 3))
            {
                _billRepository.TryAdd(bill);
            }
        }
    }
}
