using Microsoft.EntityFrameworkCore;
using TextApp.Data;
using TextApp.Dtos.GroupDtos;
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

        public async Task<GroupProfiles> GetGroupNameAndProfilesAsync(Guid groupId)
        {
            GroupProfiles result = new();

            Group group = await _context.Groups.FindAsync(groupId);

            if (group != null) 
            {
                Profile[] groupProfiles = [];

                for (int i = 0; i < group.Members.Length; i++)
                {
                    Profile profile = await _context.Profiles.FindAsync(group.Members[i].ToString());

                    if (profile != null)
                    {
                        groupProfiles = [.. groupProfiles, profile];
                    }
                    else
                    {
                        continue;
                    }
                }

                result.Name = group.Name;
                result.Profiles = groupProfiles;

                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task<Group[]> GetUserGroupsAsync(Guid[]? groupIds)
        {
            Group[] groups = [];

            if (groupIds == null)
            {
                return groups;
            }

            for (int i = 0; i < groupIds.Length; i++)
            {
                Group group = await _context.Groups.FindAsync(groupIds.ElementAt(i));
                groups = [.. groups, group];
            }

            return groups;
        }

        public async Task<bool> ChangeGroupNameAsync(Guid groupId, string Name)
        {
            Group group = await _context.Groups.FindAsync(groupId);

            if (group == null)
            {
                return false;
            }
            else
            {
                group.Name = Name;
                await _context.SaveChangesAsync();

                return true;
            }
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
