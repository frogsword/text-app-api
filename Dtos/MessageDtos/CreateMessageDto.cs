using System.ComponentModel.DataAnnotations;

namespace TextApp.Dtos.MessageDtos
{
    public class CreateMessageDto
    {
        [Required]
        [MinLength(1)]
        public string Body { get; set; }
    }
}
