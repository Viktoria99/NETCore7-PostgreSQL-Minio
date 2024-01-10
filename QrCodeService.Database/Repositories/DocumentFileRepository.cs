
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;


namespace QrCodeService.Database.Repositories
{
    public class DocumentFileRepository : IDocumentFileRepository
    {
        private readonly DatabaseContext _contextDb;

        public DatabaseContext ContextDb
        {
            get
            {
                return _contextDb;
            }
        }

        public DocumentFileRepository(DatabaseContext context)
        {
            _contextDb = context;
        }

        public async Task AddFileAsync(Article file)
        {
            await _contextDb.Files.AddAsync(file);
            await _contextDb.SaveChangesAsync();
        }

        public Article FindFile(int articleId)
        {
            var file = _contextDb.Files.FirstOrDefault(f => f.ArticleId == articleId);
            return file ?? new Article();
        }
    }
}