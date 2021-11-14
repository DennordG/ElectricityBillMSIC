using System;
using System.Linq;
using System.Threading.Tasks;
using ElectricityBillMSIC.Core.Domain;
using ElectricityBillMSIC.Core.Repositories;
using ElectricityBillMSIC.Extensions;

namespace ElectricityBillMSIC.Application
{
    internal class ShowClientsGroupedByOverduenessDecisionHandler : IUserMenuDecisionHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IBillRepository _billRepository;

        public ShowClientsGroupedByOverduenessDecisionHandler(IClientRepository clientRepository, IBillRepository billRepository)
        {
            _clientRepository = clientRepository;
            _billRepository = billRepository;
        }

        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.ShowClientsGroupedByOverdueness;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            parameter.Writer.WriteLine();

            var clientsByOverdueness = _clientRepository.GetAll().ToLookup(IsOverdue);
            if (clientsByOverdueness.Count == 0)
            {
                parameter.Writer.WriteLine("No clients found.");
            }
            else
            {
                if (clientsByOverdueness.Contains(true))
                {
                    parameter.Writer.WriteLine();
                    parameter.Writer.WriteLine("Overdue clients:");
                    clientsByOverdueness[true].Print(parameter.Writer);
                }

                if (clientsByOverdueness.Contains(false))
                {
                    parameter.Writer.WriteLine();
                    parameter.Writer.WriteLine("Paid-to-date clients:");
                    clientsByOverdueness[false].Print(parameter.Writer);
                }
            }

            parameter.Writer.WriteLine();

            return Task.FromResult(false);
        }

        private bool IsOverdue(Client client)
        {
            var bills = _billRepository.GetAll(client.Code);

            return bills.Any(IsOverdue);
        }

        private static bool IsOverdue(Bill bill)
        {
            return !bill.IsPaid && bill.DateOfIssue < DateTime.Now.AddMonths(-1);
        }
    }
}
