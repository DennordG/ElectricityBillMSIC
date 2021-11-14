using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowClientBillsDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IBillRepository _billRepository;

        public ShowClientBillsDecisionHandler(IClientRepository clientRepository, IBillRepository billRepository)
        {
            _clientRepository = clientRepository;
            _billRepository = billRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowClientBills;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            parameter.Writer.WriteLine();

            parameter.Writer.Write("Input the client code: ");

            var code = parameter.Reader.ReadLine().Trim();

            var client = _clientRepository.Get(code);
            if (client != null)
            {
                _billRepository.GetAll(client.Code).Print(parameter.Writer);
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
