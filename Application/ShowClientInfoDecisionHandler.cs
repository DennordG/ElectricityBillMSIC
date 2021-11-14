using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowClientInfoDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;

        public ShowClientInfoDecisionHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowClientInfo;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            parameter.Writer.WriteLine();
            parameter.Writer.Write("Input the client code: ");

            var code = parameter.Reader.ReadLine().Trim();

            var client = _clientRepository.Get(code);
            if (client != null)
            {
                client.Print(parameter.Writer);
            }
            else
            {
                parameter.Writer.WriteLine("Client not found.");
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }
    }
}
