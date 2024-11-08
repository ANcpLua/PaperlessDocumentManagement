using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaperlessDocumentManagement.DataAccessLayer.Entities.Configurations
{
    public class DocumentVersionConfiguration : IEntityTypeConfiguration<DocumentVersion>
    {
        public void Configure(EntityTypeBuilder<DocumentVersion> builder)
        {
            builder.ToTable("DocumentVersions");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.FileId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(v => v.VersionNumber)
                   .IsRequired();

            builder.Property(v => v.ChangeDescription)
                   .HasMaxLength(500);

            builder.Property(v => v.ContentType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(v => v.FileSize)
                   .IsRequired();

            builder.HasIndex(v => new { v.DocumentId, v.VersionNumber })
                   .IsUnique();

            builder.Has
}
