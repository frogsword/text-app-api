using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TextApp.Data;
using TextApp.Dtos.MessageDtos;
using TextApp.Interfaces;
using TextApp.Mappers;
using TextApp.Models;
using TextApp.Services;

namespace TextApp.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageInterface _messageRepo;

        public MessageController
        (
            IMessageInterface messageRepo
        )
        {
            _messageRepo = messageRepo;
        }

        [HttpGet]
        [Route("{groupId:guid}")]
        [Authorize]
        //[OutputCache]
        public async Task<IActionResult> GetMessages([FromRoute] string groupId)
        {
            var guidGroupId = new Guid( groupId );
            var messages = await _messageRepo.GetAsync(guidGroupId);

            return Ok(messages);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMessageDto createMessageDto)
        {
            if (ModelState.IsValid)
            {
                var userId = Request.Cookies["user_id"];
                //set as secret
                var key = "v5fcvt72y03urf7g06ety8bfrdq75wtc";

                var decryptedString = AesService.DecryptString(key, userId);

                createMessageDto.SenderId = new Guid(decryptedString);

                var messageModel = createMessageDto.ToMessageFromCreateDto();

                await _messageRepo.CreateAsync(messageModel);

                return Ok(messageModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
