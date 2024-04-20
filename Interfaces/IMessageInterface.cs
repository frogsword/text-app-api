using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IMessageInterface
    {
        Task<List<Message>> GetAsync(Guid senderId, Guid receiverId);
        Task<Message> CreateAsync(Message messageModel);
    }
}
