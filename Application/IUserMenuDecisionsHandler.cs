using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ElectricityBillMSIC.Application
{
    public interface IUserMenuDecisionsHandler
    {
        Task<bool> HandleAsync(UserMenuDecisionHandlerParameter parameter, CancellationToken cancellationToken = default);
    }
}
