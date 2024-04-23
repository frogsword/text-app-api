using TextApp.Data;
using TextApp.Interfaces;
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

        public async Task UpdateProfileGroupsAsync(Guid groupId, string userId)
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
    }
}
