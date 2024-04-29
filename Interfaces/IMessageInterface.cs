using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IMessageInterface
    {
        Task<List<Message>> GetAsync(Guid groupId);
        Task<Message> CreateAsync(Message messageModel);
        Task<List<Message>> UpdateAsync(Guid messageId, string body);
        Task<List<Message>> DeleteAsync(Guid messageId);
    }
}
