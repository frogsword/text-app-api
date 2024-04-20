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

        public async Task<Profile> CreateAsync(Profile profileModel)
        {
            await _context.Profiles.AddAsync(profileModel);
            await _context.SaveChangesAsync();

            return profileModel;
        }
    }
}
