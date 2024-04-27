using Microsoft.EntityFrameworkCore;
using TextApp.Data;
using TextApp.Interfaces;
using TextApp.Migrations;
using TextApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextApp.Repositories
{
    public class GroupRepository : IGroupInterface
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetUserGroupsAsync(List<Guid>? userGroupIds)
        {
            List<Group> groups = [];

            if (userGroupIds.Count == 0)
            {
                return groups;
            }

            for (int i = 0; i < userGroupIds.Count; i++)
            {
               Group group = await _context.Groups.FindAsync(userGroupIds.ElementAt(i));
               groups = [.. groups, group];
            }

            return groups;
        }

        public async Task<bool> AddOrRemoveUserAsync(Guid groupId, Guid userId, string action)
        {
            Group group = await _context.Groups.FindAsync(groupId);

            if (group == null) 
            {
                return false;
            }

            if (action == "remove" && group.Members.Contains(userId))
            {
                int indexToRemove = Array.IndexOf(group.Members, userId);
                group.Members = group.Members.Where((z, x) => x != indexToRemove).ToArray();
            }
            else if (action == "add")
            {
                group.Members = [.. group.Members, userId];
            }
            else
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Group> CreateAsync(Group groupModel)
        {
            var group = await _context.Groups.AddAsync(groupModel);

            await _context.SaveChangesAsync();

            return group.Entity;
        }
    }
}
