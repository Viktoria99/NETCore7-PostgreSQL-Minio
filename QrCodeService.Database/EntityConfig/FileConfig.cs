using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrCodeService.Domain.Entity;


namespace QrCodeService.Database.EntityConfig
{
    public sealed class FileConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Files")
               .ToTable(t => t.HasComment("File information"));
            builder.Property(p => p.Id)
               .HasComment("Record ID");
            builder.Property(p => p.ArticleId)
               .HasComment("Article Id");
            builder.Property(p => p.Name)
               .HasComment("The name of the uploaded file");
            builder.Property(p => p.OperatorFileId)
               .HasComment("Id assigned when uploading the file");
        }
    }
}