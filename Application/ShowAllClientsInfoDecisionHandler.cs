using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowAllClientsInfoDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;

        public ShowAllClientsInfoDecisionHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowAllClientsInfo;

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
