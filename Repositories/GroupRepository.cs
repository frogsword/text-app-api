using Microsoft.EntityFrameworkCore;
using TextApp.Data;
using TextApp.Interfaces;
using TextApp.Migrations;
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

        public async Task<List<Group>> GetUserGroupsAsync(List<Guid> userGroupIds)
        {
            List<Group> groups = [];
            for (int i = 0; i < userGroupIds.Count; i++)
            {
               Group group = await _context.Groups.FindAsync(userGroupIds.ElementAt(i));
               groups = [.. groups, group];
            }

            return groups;
        }

        public async Task<Group> UpdateGroupAsync(Group groupModel)
        {
            var group = await _context.Groups.FindAsync(groupModel.Id);

            group.Members = groupModel.Members;
            group.Name = groupModel.Name;

            await _context.SaveChangesAsync();

            return group;
        }

        public async Task<Group> CreateAsync(Group groupModel)
        {
            var group = await _context.Groups.AddAsync(groupModel);

            await _context.SaveChangesAsync();

            return group.Entity;
        }
    }
}
