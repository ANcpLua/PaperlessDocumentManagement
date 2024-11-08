using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaperlessDocumentManagement.DataAccessLayer.Entities.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Title)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(d => d.FileId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.ContentType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.FileSize)
                   .IsRequired();

            builder.Property(d => d.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(d => d.OcrText)
                   .HasColumnType("text");

            builder.Property(d => d.Metadata)
                   .HasColumnType("jsonb");

            builder.HasQueryFilter(d => !d.IsDeleted);

            builder.HasMany(d => d.Tags)
                   .WithMany(t => t.Documents)
                   .UsingEntity(j => j.ToTable("DocumentTags"));

            builder.HasMany(d => d.Versions)
                   .WithOne(v => v.Document)
                   .HasForeignKey(v => v.DocumentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
