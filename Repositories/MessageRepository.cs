using Microsoft.EntityFrameworkCore;
using TextApp.Data;
using TextApp.Interfaces;
using TextApp.Models;

namespace TextApp.Repositories
{
    public class MessageRepository : IMessageInterface
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAsync(Guid senderId, Guid receiverId)
        {
            var senderMessages = await _context.Messages
                .Where((m => m.Sender == senderId && m.Receiver == receiverId))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            var receiverMessages = await _context.Messages
                .Where((m => m.Sender == receiverId && m.Receiver == senderId))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            senderMessages.AddRange(receiverMessages);

            List<Message> messages = senderMessages.OrderBy(m => m.CreatedAt).ToList();

            return messages;
        }

        public async Task<Message> CreateAsync(Message messageModel)
        {
            await _context.Messages.AddAsync(messageModel);
            await _context.SaveChangesAsync();

            return messageModel;
        }
    }
}
