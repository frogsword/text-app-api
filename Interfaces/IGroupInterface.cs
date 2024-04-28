using Microsoft.AspNetCore.Mvc;
using TextApp.Dtos.GroupDtos;
using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IGroupInterface
    {
        Task<GroupProfiles> GetGroupNameAndProfilesAsync(Guid groupId);
        Task<Group[]> GetUserGroupsAsync(Guid[]? groupIds);
        Task<bool> ChangeGroupNameAsync(Guid groupId, string Name);
        Task<bool> AddOrRemoveUserAsync(Guid groupId, Guid userId, string action);
        Task<Group> CreateAsync(Group groupModel);
    }
}
