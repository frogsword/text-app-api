using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IGroupInterface
    {
        Task<List<Group>> GetUserGroupsAsync(List<Guid> userGroupIds);
        Task<Group> UpdateGroupAsync(Group groupModel);
        Task<Group> CreateAsync(Group groupModel);
    }
}
