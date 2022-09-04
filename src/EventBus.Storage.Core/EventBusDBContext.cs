using Castle.DynamicProxy;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Storage.Core
{
    internal class EventBusDBContext : DbContext
    {
        public EventBusDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var entityTypes = assemblies.SelectMany(assemblie => assemblie.GetTypes().Where(t => t.HasInterface<IEntity>())).ToArray();
            var types = entityTypes.Where(a => a.HasInterface<IProxyTargetAccessor>() == false).ToArray();  // 去除懒加载创建的实体代理
            foreach (var t in types)
            {
                if (t.IsAbstract) continue;

                modelBuilder.Entity(t);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
