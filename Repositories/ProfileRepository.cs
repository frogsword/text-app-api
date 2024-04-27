using TextApp.Data;
using TextApp.Interfaces;
using TextApp.Migrations;
using TextApp.Models;

namespace TextApp.Repositories
{
    public class ProfileRepository : IProfileInterface
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profile> GetAsync(string userId)
        {
            Profile profile = await _context.Profiles.FindAsync(userId);

            return profile;
        }

        public async Task<Profile> CreateAsync(Profile profileModel)
        {
            await _context.Profiles.AddAsync(profileModel);
            await _context.SaveChangesAsync();

            return profileModel;
        }

        public async Task AddGroupToProfileAsync(Guid groupId, string userId)
        {
            Profile? profile = await _context.Profiles.FindAsync(userId);

            Guid[]? groups = profile?.Groups;

            if (profile != null && groups == null)
            {
                profile.Groups = [groupId];
            }
            else if (profile != null && groups != null)
            {
                profile.Groups = [.. groups, groupId];
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveGroupFromProfileAsync(Guid groupId, string userId)
        {
            Profile profile = await _context.Profiles.FindAsync(userId);

            int indexToRemove = Array.IndexOf(profile.Groups, groupId);
            profile.Groups = profile.Groups.Where((z, x) => x != indexToRemove).ToArray();

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsernameAsync(string userId, string username)
        {
            Profile profile = await _context.Profiles.FindAsync(userId);

            profile.Username = username;

            await _context.SaveChangesAsync();
        }
    }
}
