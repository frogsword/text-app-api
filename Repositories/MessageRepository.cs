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

        public async Task<List<Message>> GetAsync(Guid groupId)
        {
            var messages = await _context.Messages.Where(m => m.GroupId == groupId).ToListAsync();

            return messages;
        }

        public async Task<Message> CreateAsync(Message messageModel)
        {
            await _context.Messages.AddAsync(messageModel);
            await _context.SaveChangesAsync();

            return messageModel;
        }

        public async Task<List<Message>> UpdateAsync(Guid messageId, string body)
        {
            Message message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

            if (message != null)
            {
                message.Body = body;
                await _context.SaveChangesAsync();

                var messages = await _context.Messages.Where(m => m.GroupId == message.GroupId).ToListAsync();

                return messages;
            }
            else
            {
                return [];
            }
        }

        public async Task<List<Message>> DeleteAsync(Guid messageId)
        {
            Message message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

            if (message != null)
            {
                message.IsDeleted = true;
                await _context.SaveChangesAsync();

                var messages = await _context.Messages.Where(m => m.GroupId == message.GroupId).ToListAsync();

                return messages;
            }
            else
            {
                return [];
            }
        }
    }
}
