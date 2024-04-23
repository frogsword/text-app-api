using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Dtos.GroupDtos;
using TextApp.Dtos.MessageDtos;
using TextApp.Interfaces;
using TextApp.Mappers;
using TextApp.Models;
using TextApp.Services;

namespace TextApp.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupInterface _groupRepo;
        private readonly IProfileInterface _profileRepo;

        public GroupController
        (
            IGroupInterface groupRepo,
            IProfileInterface profileRepo
        )
        {
            _groupRepo = groupRepo;
            _profileRepo = profileRepo;
        }

        [HttpPatch]
        public async Task<IActionResult> GetUserGroups([FromBody] List<Guid> userGroupIds)
        {
            List<Group> groups = await _groupRepo.GetUserGroupsAsync(userGroupIds);

            return Ok(groups);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] CreateGroupDto groupDto)
        {
            Group group = groupDto.ToGroupFromCreateDto();

            var result = await _groupRepo.UpdateGroupAsync(group);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto createGroupDto)
        {
            if (ModelState.IsValid)
            {
                var groupModel = createGroupDto.ToGroupFromCreateDto();

                Group group = await _groupRepo.CreateAsync(groupModel);

                //add to profiles
                Guid groupId = group.Id;
                for (var i = 0; i < group.Members.Length; i++)
                {
                    string memberId = group.Members[i].ToString();
                    await _profileRepo.AddGroupToProfileAsync(groupId, memberId);
                }

                return Ok(group);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
