using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrCodeService.Domain.Entity;

namespace QrCodeService.Database.EntityConfig
{
    public sealed class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages")
                .ToTable(t => t.HasComment("Storing requests"));
            builder.Property(p => p.Id)
                .HasComment("Record ID");
            builder.Property(p => p.ArticleId)
               .HasComment("Article ID");
            builder.Property(p => p.ArticleType)
               .HasComment("Article Type");
            builder.Property(p => p.RouteId)
               .HasComment("Route ID");
            builder.Property(p => p.Created)
                .HasComment("Time create ");
            builder.Property(p => p.Updated)
                .HasComment("Time update");
            builder.Property(p => p.OperatorMessageId)
                .HasComment("ID given by the operator to the sent message");
            builder.Property(p => p.Status)
                .HasComment("Event state. 0-send to hub;1-success;2-error;");
            builder.Property(p => p.Data)
                .HasComment("Request body");
            builder.Property(p => p.Result)
                .HasComment("The result of executing the request.Contains a response or error.");
        }
    }
}