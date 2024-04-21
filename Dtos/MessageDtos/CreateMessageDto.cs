using System.ComponentModel.DataAnnotations;

namespace TextApp.Dtos.MessageDtos
{
    public class CreateMessageDto
    {
        [Required]
        [MinLength(1)]
        public string Body { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}
