using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IGroupInterface
    {
        Task<Group> CreateAsync(Group groupModel);
    }
}
