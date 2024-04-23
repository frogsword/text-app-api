using Microsoft.EntityFrameworkCore;
using TextApp.Data;
using TextApp.Interfaces;
using TextApp.Models;

namespace TextApp.Repositories
{
    public class GroupRepository : IGroupInterface
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateAsync(Group groupModel)
        {
            var group = await _context.Groups.AddAsync(groupModel);

            await _context.SaveChangesAsync();

            return group.Entity;
        }
    }
}
