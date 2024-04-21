using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Dtos.GroupDtos;
using TextApp.Dtos.MessageDtos;
using TextApp.Interfaces;
using TextApp.Mappers;

namespace TextApp.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupInterface _groupRepo;

        public GroupController
        (
            IGroupInterface groupRepo
        )
        {
            _groupRepo = groupRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto createGroupDto)
        {
            if (ModelState.IsValid)
            {
                var groupModel = createGroupDto.ToGroupFromCreateDto();

                await _groupRepo.CreateAsync(groupModel);

                return Ok(groupModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
