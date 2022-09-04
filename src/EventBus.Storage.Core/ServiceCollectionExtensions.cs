using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var storageSection = configuration.GetSection(nameof(Storage));
            var storageTypeValue = storageSection.GetSection(nameof(Storage.StorageType)).Value;
            var r = Enum.TryParse(storageTypeValue, true, out StorageType storageType);
            if (r == false)
            {
                storageType = StorageType.Sqlite;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unrecognizable StorageType:{storageTypeValue}. Enable default database:Sqlite.");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Database: {storageType}");

            var connectionString = storageSection.GetSection(nameof(Storage.ConnectionString)).Value;
            if (connectionString.IsNullOrEmpty())
                throw new NullReferenceException("Database ConnectionString can not be empty.");

            services.AddDbContext<EventBusDBContext>(options =>
            {
                var builder = options.UseLazyLoadingProxies();
                if (storageType == StorageType.Sqlite)
                {
                    builder.UseSqlite(connectionString);
                }

                if (storageType == StorageType.MySql)
                {
                    var serverVersiohn = ServerVersion.AutoDetect(connectionString);
                    builder.UseMySql(connectionString, serverVersiohn);
                }

                if (storageType == StorageType.SqlServer)
                {
                    builder.UseSqlServer(connectionString);
                }

            });
            services.AddScoped<IRepository, Repository>();



            return services;
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder InitializationDatabase(this IApplicationBuilder applicationBuilder)
        {
            var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var db = serviceScope.ServiceProvider.GetRequiredService<EventBusDBContext>();
            db.Database.EnsureCreated();
            return applicationBuilder;
        }
    }
}
