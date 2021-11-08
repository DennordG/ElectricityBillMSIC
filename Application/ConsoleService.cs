using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElectricityBillMSIC.Application
{
    internal class ConsoleService
    {
        private readonly IUserMenuDecisionsHandler _decisionHandler;

        public ConsoleService(IUserMenuDecisionsHandler decisionHandler)
        {
            _decisionHandler = decisionHandler;
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            var isExitTriggered = false;
            while (!isExitTriggered)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add new client");
                Console.WriteLine("2. Show client");
                Console.WriteLine("3. Show all clients");
                Console.WriteLine("Esc. Exit");

                var userMenuDecision = UserMenuDecisionParser.GetUserMenuDecisionFromConsole();

                var decisionHandlerParameter = GetDecisionHandlerParameter(userMenuDecision);

                cancellationToken.ThrowIfCancellationRequested();

                isExitTriggered = await _decisionHandler.HandleAsync(decisionHandlerParameter, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        private static UserMenuDecisionHandlerParameter GetDecisionHandlerParameter(UserMenuDecisionType userMenuDecision)
        {
            return new UserMenuDecisionHandlerParameter
            {
                DecisionType = userMenuDecision,
                Reader = Console.In,
                Writer = Console.Out
            };
        }
    }
}
