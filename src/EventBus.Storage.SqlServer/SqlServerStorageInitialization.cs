using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.SqlServer
{
    internal class SqlServerStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SqlServerDBContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
            services.AddScoped<IRepository, SqlServerRepository>();
        }
    }
}
