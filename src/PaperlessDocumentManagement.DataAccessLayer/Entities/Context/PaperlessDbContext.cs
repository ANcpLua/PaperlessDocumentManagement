using Microsoft.EntityFrameworkCore;
using PaperlessDocumentManagement.DataAccessLayer.Entities.Configurations;

namespace PaperlessDocumentManagement.DataAccessLayer.Entities.Context
{
    public class PaperlessDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<DocumentVersion> DocumentVersions { get; set; } = null!;

        public PaperlessDbContext(DbContextOptions<PaperlessDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentVersionConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
