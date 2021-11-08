using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Repositories;

namespace ElectricityBillMSIC.Application
{
    internal class AddNewClientDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;

        public AddNewClientDecisionHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.AddNewClient;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            var newClient = AppFaker.GenerateClient();

            parameter.Writer.WriteLine();

            if (_clientRepository.TryAdd(newClient))
            {
                parameter.Writer.WriteLine("Generated new client with Code {0}", newClient.Code);
            }
            else
            {
                parameter.Writer.WriteLine("Failed to generate a new client");
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }
    }
}
