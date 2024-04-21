using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IMessageInterface
    {
        Task<List<Message>> GetAsync(Guid groupId);
        Task<Message> CreateAsync(Message messageModel);
    }
}
