using QrCodeService.Domain.Entity;

namespace QrCodeService.Database.Repositories.Interfaces
{
    public interface IDocumentFileRepository
    {
        Task AddFileAsync(Article file);

        Article FindFile(int articleId);

        DatabaseContext ContextDb { get; }
    }
}