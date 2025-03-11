using Microsoft.Extensions.DependencyInjection;
using Transaction.Application.Contracts;
using Transaction.Infrastructure.Extensions;

namespace Transaction.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
        {
            services.AddScoped<ITransaction, Features.Transaction>();
            return services.AddInfrastructureDependency();
        }
    }
}
