using System.ComponentModel.DataAnnotations;

namespace TextApp.Dtos.MessageDtos
{
    public class CreateMessageDto
    {
        [Required]
        [MinLength(1)]
        public string Body { get; set; }

        public Guid SenderId { get; set; }

        public string SenderUsername { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}
