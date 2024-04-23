using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IProfileInterface
    {
        Task<Profile> GetAsync(string userId);
        Task<Profile> CreateAsync(Profile profileModel);
        Task AddGroupToProfileAsync(Guid groupId, string userId);
        Task RemoveGroupFromProfileAsync(string userId, List<Guid> groupIds);
    }
}
