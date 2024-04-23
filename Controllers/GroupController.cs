using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Dtos.GroupDtos;
using TextApp.Dtos.MessageDtos;
using TextApp.Interfaces;
using TextApp.Mappers;
using TextApp.Models;

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
                    await _profileRepo.UpdateProfileGroupsAsync(groupId, memberId);
                }

                //returns group object with id and members
                return Ok(group);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
