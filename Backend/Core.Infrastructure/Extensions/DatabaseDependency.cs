using Core.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Extensions
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddDatabaseDependency(this IServiceCollection services)
        {
            return services.AddSingleton<DapperContext>();
        }
    }
}
