using TextApp.Dtos.MessageDtos;
using TextApp.Models;

namespace TextApp.Mappers
{
    public static class MessageMapper
    {
        public static Message ToMessageFromCreateDto(this CreateMessageDto messageDto)
        {
            return new Message
            {
                Body = messageDto.Body,
                Sender = messageDto.SenderId,
                GroupId = messageDto.GroupId,
                SenderUsername = messageDto.SenderUsername,
            };
        }
    }
}
