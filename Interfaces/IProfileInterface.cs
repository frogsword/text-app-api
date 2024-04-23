using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IProfileInterface
    {
        Task<Profile> GetAsync(string userId);
        Task<Profile> CreateAsync(Profile profileModel);
        Task UpdateProfileGroupsAsync(Guid groupId, string userId);
    }
}
