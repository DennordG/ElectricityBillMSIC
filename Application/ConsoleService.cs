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
                Console.WriteLine("2. Show client info");
                Console.WriteLine("3. Show client bills");
                Console.WriteLine("4. Show client bills by address");
                Console.WriteLine("5. Show clients grouped by overdueness");
                Console.WriteLine("6. Show clients that have to pay more than");
                Console.WriteLine("Esc. Exit");

                var userMenuDecision = UserMenuDecisionParser.GetUserMenuDecisionFromConsole();
                if (userMenuDecision == UserMenuDecisionType.ClearConsole)
                {
                    Console.Clear();
                    continue;
                }

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
