using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EventBus.Storage.Sqlite
{
    internal class SqliteDBContext: DbContext
    {
        public SqliteDBContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
