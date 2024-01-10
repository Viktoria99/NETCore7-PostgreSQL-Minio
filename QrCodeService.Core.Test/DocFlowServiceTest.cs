using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.Services.Implementations;
using QrCodeService.Database;
using QrCodeService.Database.Repositories;
using Xunit;

namespace QrCodeService.Core.Test
{
    public class DocFlowServiceTest
    {
        private DocFlowService _flowService;

        [Theory]
        [InlineData("client.error", "d6ef46f0000708eef68e884", "klm", "f24775bd09rptyu6")]

        public async Task ProcessAsyncTest(string code, string parent, string producer, string consumer)
        {
            InjectionServices();
            var res = new GetEventResponse
            {
                Status = 200,
                Article = "Information about sender",
                Events = new List<GetEventResponseEvents>()
                {
                   new GetEventResponseEvents {
                     Id = "c6dfghhj5cd2lxvb9b4",
                     Code = code,
                     Parent = parent,
                     Producer = producer,
                     Consumer = new string[] { consumer },
                     Created = "2023-10-27",
                     Data = new {
                        id = "2dfgdfg84r8",
                        code = "13",
                        type = @"https://*****.ru/documentation",
                        article = "Error checking file tag",
                        detail = "File has an incorrect format",
                        status = 400,
                        created = @"2024-01-10"
                     }
                   }
               }
            };

            await _flowService.ProcessAsync(res);
        }


        public void InjectionServices()
        {
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<DatabaseContext>((sp, options) =>
            options.UseInMemoryDatabase(databaseName: "Database")
            .UseInternalServiceProvider(sp)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped);
            var serviceProvider = services.BuildServiceProvider();

            var _context = serviceProvider.GetRequiredService<DatabaseContext>();
            var messageRepository = new MessageRepository(_context);
            _flowService = new DocFlowService(messageRepository);
        }
    }
}
