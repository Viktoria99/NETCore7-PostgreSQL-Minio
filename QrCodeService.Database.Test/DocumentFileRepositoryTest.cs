using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using QrCodeService.Database.Repositories;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;
namespace QrCodeService.Database.Test
{
    [TestFixture]
    [Author("Person", "person@example.com")]
    public class DocumentFileRepositoryTest
    {
        private DatabaseContext _context;

        [TestCase(8888, "fdfsgsgsdg", "4664"), Description("Example of test. Saving file")]
        public void FindFileTest(int articleId, string urlXmlFile, string fileId)
        {
            GetContext();
            var article = new Article()
            {
                Name = urlXmlFile,
                OperatorFileId = fileId,
                ArticleId = articleId
            };
            _context.Files.Add(article);
            _context.SaveChanges();
            var file = _context.Files.FirstOrDefault(f => f.ArticleId == articleId);

        }
        public void GetContext()
        {
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<DatabaseContext>((sp, options) =>
            options.UseInMemoryDatabase(databaseName: "Database")
            .UseInternalServiceProvider(sp)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped);

            services.AddScoped<IDocumentFileRepository, DocumentFileRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<DatabaseContext>();

        }
    }
}