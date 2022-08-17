using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.Sqlite
{
    internal class SqliteStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SqliteDBContext>(builder =>
            {
                builder.UseLazyLoadingProxies().UseSqlite(connectionString);
            });
            services.AddScoped<IRepository, SqliteRepository>();
        }
    }
}
