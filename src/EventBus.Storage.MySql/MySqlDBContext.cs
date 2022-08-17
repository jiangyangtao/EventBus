using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EventBus.Storage.MySql
{
    internal class MySqlDBContext : DbContext
    {
        public MySqlDBContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
