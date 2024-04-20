using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Data;
using TextApp.Dtos.MessageDtos;
using TextApp.Interfaces;
using TextApp.Mappers;
using TextApp.Models;

namespace TextApp.Controllers
{
    [Route("api/[controller]")]
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
        [Route("{senderId:guid}/{receiverId:guid}")]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetMessages([FromRoute] Guid senderId, [FromRoute] Guid receiverId)
        {
            var messages = await _messageRepo.GetAsync(senderId, receiverId);

            return Ok(messages);
        }

        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMessageDto createMessageDto)
        {
            if (ModelState.IsValid)
            {
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
