using System.Threading.Tasks;

namespace ElectricityBillMSIC.Application
{
    internal class ExitDecisionHandler : IUserMenuDecisionHandler
    {
        public UserMenuDecisionType ApplicableType => UserMenuDecisionType.Exit;

        public Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter)
        {
            return Task.FromResult(true);
        }
    }
}
