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
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEntity)))).ToArray();
            foreach (var v in types)
            {
                if (v.IsAbstract) continue;

                modelBuilder.Entity(v);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
