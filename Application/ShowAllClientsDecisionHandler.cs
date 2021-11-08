using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowAllClientsDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;

        public ShowAllClientsDecisionHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowAllClients;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            var clients = _clientRepository.GetAll();

            parameter.Writer.WriteLine();

            if (clients.Count > 0)
            {
                clients.Print(parameter.Writer);
            }
            else
            {
                parameter.Writer.WriteLine("No clients available.");
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }
    }
}
