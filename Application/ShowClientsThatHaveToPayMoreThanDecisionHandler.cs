using System.Linq;
using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Domain;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowClientsThatHaveToPayMoreThanDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IBillRepository _billRepository;

        public ShowClientsThatHaveToPayMoreThanDecisionHandler(IClientRepository clientRepository, IBillRepository billRepository)
        {
            _clientRepository = clientRepository;
            _billRepository = billRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowClientsThatHaveToPayMoreThan;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            parameter.Writer.WriteLine();

            parameter.Writer.Write("Input the amount threshold: ");

            var threshold = double.MaxValue;
            var inputIsValid = false;
            while (!inputIsValid)
            {
                var input = parameter.Reader.ReadLine().Trim();
                if (double.TryParse(input, out threshold))
                {
                    inputIsValid = true;
                }
                else
                {
                    parameter.Writer.WriteLine("Please input a valid amount.");
                }
            }

            var clients = _clientRepository.GetAll().Where(c => HasToPayMoreThan(c, threshold)).ToList();
            if (clients.Count > 0)
            {
                clients.Print(parameter.Writer);
            }
            else
            {
                parameter.Writer.WriteLine("No clients have to pay more than the input amount.");
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }

        private bool HasToPayMoreThan(Client client, double threshold)
        {
            return _billRepository.GetAll(client.Code).Where(b => !b.IsPaid).Sum(b => b.Cost) > threshold;
        }
    }
}
