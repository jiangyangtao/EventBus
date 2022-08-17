using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EventBus.Storage.SqlServer
{
    internal class SqlServerDBContext: DbContext
    {
        public SqlServerDBContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
