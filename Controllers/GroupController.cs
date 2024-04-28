using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Dtos.GroupDtos;
using TextApp.Dtos.MessageDtos;
using TextApp.Interfaces;
using TextApp.Mappers;
using TextApp.Migrations;
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
        private readonly IConfiguration _config;

        public GroupController
        (
            IGroupInterface groupRepo,
            IProfileInterface profileRepo,
            IConfiguration config
        )
        {
            _groupRepo = groupRepo;
            _profileRepo = profileRepo;
            _config = config;
        }
        
        //endpoint to get group name, user names and associated ids
        [HttpGet("{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid groupId)
        {
            var result = await _groupRepo.GetGroupNameAndProfilesAsync(groupId);

            return Ok(result);
        }

        //change to endpoint for changing group name
        [HttpPatch("{groupId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUserGroups([FromRoute] Guid groupId, [FromBody] string name)
        {
            bool succeeded = await _groupRepo.ChangeGroupNameAsync(groupId, name);

            if (succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{groupId:guid}/users/{userId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateGroup([FromRoute] Guid groupId, [FromRoute] Guid userId, [FromBody] string action)
        {
            //action will either be 'remove' or 'add'
            bool result = await _groupRepo.AddOrRemoveUserAsync(groupId, userId, action);

            if (result && action == "add")
            {
                //call profile repo to add group to user profile
                await _profileRepo.AddGroupToProfileAsync(groupId, userId.ToString());

                return Ok();
            }
            else if (result && action == "remove")
            {
                //call profile repo to add group to user profile
                await _profileRepo.RemoveGroupFromProfileAsync(groupId, userId.ToString());

                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto createGroupDto)
        {
            if (ModelState.IsValid)
            {
                var userId = Request.Cookies["user_id"];

                var groupModel = createGroupDto.ToGroupFromCreateDto();

                //set as secret
                var key = _config["AesKey"];
                var decryptedString = AesService.DecryptString(key, userId);
                var id = new Guid(decryptedString);
                createGroupDto.Members = [.. createGroupDto.Members, id];

                try
                {
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
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
