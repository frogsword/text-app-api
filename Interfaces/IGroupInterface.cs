using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IGroupInterface
    {
        Task<List<Group>> GetUserGroupsAsync(List<Guid> userGroupIds);
        Task<Group> CreateAsync(Group groupModel);
    }
}
