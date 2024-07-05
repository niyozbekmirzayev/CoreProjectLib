using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Core.Data
{
    public class BaseDbContext : DbContext
    {
        private string schemaName = "public";

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public BaseDbContext(DbContextOptions options, string schemaName) : base(options)
        {
            if (string.IsNullOrWhiteSpace(schemaName))
            {
                throw new Exception("database schemaName can not be null or white space");
            }

            this.schemaName = schemaName;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);

            base.OnModelCreating(modelBuilder);

            ConfigurePersistentEntity(modelBuilder);
        }

        private void ConfigurePersistentEntity(ModelBuilder modelBuilder)
        {
            var configureMethod = typeof(BaseDbContext).GetTypeInfo().DeclaredMethods
                .First(m => m.Name == nameof(ConfigurePersistentEntity) && m.IsGenericMethod);

            foreach (IMutableEntityType mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(PersistentEntity)))
                {
                    configureMethod.MakeGenericMethod(mutableEntityType.ClrType).Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void ConfigurePersistentEntity<TEntity>(ModelBuilder modelBuilder) where TEntity : PersistentEntity
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasQueryFilter(f => f.State != Enums.EntityState.Deleted);
            });

            modelBuilder.Entity<TEntity>()
                .Property(e => e.State)
                .HasConversion<string>();
        }
    }
}
