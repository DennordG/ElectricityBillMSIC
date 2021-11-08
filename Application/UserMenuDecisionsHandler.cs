using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElectricityBillMSIC.Application
{
    internal class UserMenuDecisionsHandler : IUserMenuDecisionsHandler
    {
        private readonly Dictionary<UserMenuDecisionType, IUserMenuDecisionHandler> _handlersByApplicability;

        public UserMenuDecisionsHandler(IEnumerable<IUserMenuDecisionHandler> handlers)
        {
            _handlersByApplicability = handlers.ToDictionary(h => h.ApplicableType);
        }

        public async Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter, CancellationToken cancellationToken = default)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (!_handlersByApplicability.TryGetValue(parameter.DecisionType, out var handler))
            {
                throw new InvalidOperationException($"Unhandled user decision {parameter.DecisionType}");
            }

            return await handler.HandleAsync(parameter);
        }
    }
}
