using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.IRepositories;
using EventBus.Storage.Mysql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.MySql
{
    internal class MySqlStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MySqlDBContext>(options =>
            {
                var serverVersiohn = ServerVersion.AutoDetect(connectionString);
                options.UseLazyLoadingProxies().UseMySql(connectionString, serverVersiohn);
            });
            services.AddScoped<IRepository, MySqlRepository>();
        }
    }
}
