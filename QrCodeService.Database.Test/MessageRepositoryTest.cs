using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using QrCodeService.Database.Repositories;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;
using QrCodeService.Domain.Types;

namespace QrCodeService.Database.Test
{
    [TestFixture]
    [Author("Person", "person@example.com")]

    public class MessageRepositoryTest
    {
        private DatabaseContext _context;

        [TestCaseSource(nameof(TestCases))]
        public async void UpdateMessageStatusTest((Message msg, string Id) paramName)
        {
            GetContext();
            _context.Messages.Add(paramName.msg);
            _context.SaveChanges();
            var messageValue = _context.Messages.First(f => f.OperatorMessageId == paramName.Id);
            Assert.That(messageValue != null, Is.True);
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

            services.AddScoped<IMessageRepository, MessageRepository>();
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetService<DatabaseContext>();
        }

        public static IEnumerable<(Message, string)> TestCases()
        {
            yield return (
            new Message
            {
                ArticleId = 4564,
                ArticleType = "Ohr",
                RouteId = 4645,
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Status = (int)MessageStatus.Send,
                OperatorMessageId = "f8d",
                Data = "errorTest"
            }, "f8d");
            yield return (
            new Message
            {
                ArticleId = 4564,
                ArticleType = "OKM",
                RouteId = 4645,
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Status = (int)MessageStatus.Send,
                OperatorMessageId = string.Empty,
                Data = string.Empty
            }, string.Empty);
        }
    }
}