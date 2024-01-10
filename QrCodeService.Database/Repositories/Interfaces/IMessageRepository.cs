using QrCodeService.Domain.Entity;

namespace QrCodeService.Database.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);

        Task UpdateMessagesStatus(List<string> operatorMessageId, string error);

        Message SelectMessage(string OperatorMessageId);

    }
}