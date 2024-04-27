using Microsoft.AspNetCore.Mvc;
using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IGroupInterface
    {
        Task<List<Group>> GetUserGroupsAsync(List<Guid>? userGroupIds);
        Task<bool> AddOrRemoveUserAsync(Guid groupId, Guid userId, string action);
        Task<Group> CreateAsync(Group groupModel);
    }
}
