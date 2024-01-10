using Microsoft.EntityFrameworkCore;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;
using QrCodeService.Domain.Types;

namespace QrCodeService.Database.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        protected readonly DatabaseContext _context;

        public MessageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessagesStatus(List<string> operatorMessageId, string error)
        {
            if (operatorMessageId.Count() == 0) return;
            var values = _context.Messages.Where(f => operatorMessageId.Contains(f.OperatorMessageId));
            await values.ExecuteUpdateAsync(x => x
                     .SetProperty(m => m.Status, (error == string.Empty) ? (int)MessageStatus.HandleSuccess : (int)MessageStatus.HandleError)
                     .SetProperty(m => m.Result, error));
            await _context.SaveChangesAsync();
        }


        public Message SelectMessage(string OperatorMessageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.OperatorMessageId == OperatorMessageId);
            return message ?? new Message();
        }
    }
}