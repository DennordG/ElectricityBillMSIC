using ElectricityBillMSIC.Application;
using ElectricityBillMSIC.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ElectricityBillMSIC.Extensions
{
    internal static class IocHelper
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services.AddRepositories().AddUserMenuDecisionHandlers();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddSingleton<IClientRepository, ClientDictionaryRepository>()
                           .AddSingleton<ISubscriptionRepository, SubscriptionDictionaryRepository>()
                           .AddSingleton<IBillRepository, BillDictionaryRepository>();
        }

        private static IServiceCollection AddUserMenuDecisionHandlers(this IServiceCollection services)
        {
            return services.AddTransient<IUserMenuDecisionHandler, AddNewClientDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ShowClientInfoDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ShowAllClientsInfoDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ShowClientBillsDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ShowClientBillsByAddressDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ShowClientsGroupedByOverduenessDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ShowClientsThatHaveToPayMoreThanDecisionHandler>()
                           .AddTransient<IUserMenuDecisionHandler, ExitDecisionHandler>()
                           .AddTransient<IUserMenuDecisionsHandler, UserMenuDecisionsHandler>();
        }
    }
}
