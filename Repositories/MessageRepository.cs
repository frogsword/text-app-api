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
    }
}
