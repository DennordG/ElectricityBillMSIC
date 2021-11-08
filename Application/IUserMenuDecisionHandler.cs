using System.Threading.Tasks;

namespace ElectricityBillMSIC.Application
{
    public interface IUserMenuDecisionHandler
    {
        UserMenuDecisionType ApplicableType { get; }

        Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter);
    }
}
