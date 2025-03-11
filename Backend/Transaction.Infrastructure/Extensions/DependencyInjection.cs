using Core.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Domain.Repositories;
using Transaction.Infrastructure.Transaction;

namespace Transaction.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services.AddDatabaseDependency();
        }
    }
}
