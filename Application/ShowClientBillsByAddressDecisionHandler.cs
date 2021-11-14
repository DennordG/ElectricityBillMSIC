using System;
using System.Linq;
using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowClientBillsByAddressDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IBillRepository _billRepository;

        public ShowClientBillsByAddressDecisionHandler(IClientRepository clientRepository, IBillRepository billRepository)
        {
            _clientRepository = clientRepository;
            _billRepository = billRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowClientBillsByAddress;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            parameter.Writer.WriteLine();

            parameter.Writer.Write("Input the address: ");

            var address = parameter.Reader.ReadLine().Trim();

            var clients = _clientRepository.GetAll().Where(c => c.Address.Contains(address, StringComparison.OrdinalIgnoreCase)).ToList();
            if (clients.Count > 0)
            {
                var chosenClient = clients[0];

                if (clients.Count > 1)
                {
                    parameter.Writer.WriteLine("Found multiple addresses, choose one of the following (or Enter to choose none):");
                    clients.Do((c, i) => parameter.Writer.WriteLine("{0}: {1}", i + 1, c.Address));

                    var isValidOption = false;
                    while (!isValidOption)
                    {
                        var optionStr = parameter.Reader.ReadLine().Trim();
                        if (string.IsNullOrWhiteSpace(optionStr))
                        {
                            isValidOption = true;
                            chosenClient = null;
                        }
                        else if (int.TryParse(optionStr, out var option) && 0 < option && option <= clients.Count)
                        {
                            isValidOption = true;
                            chosenClient = clients[option - 1];
                        }
                    }
                }

                if (chosenClient != null)
                {
                    parameter.Writer.WriteLine("Bills for client at address: {0}", chosenClient.Address);
                    _billRepository.GetAll(chosenClient.Code).Print(parameter.Writer);
                }
            }
            else
            {
                parameter.Writer.WriteLine("No clients found for that address.");
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }
    }
}
