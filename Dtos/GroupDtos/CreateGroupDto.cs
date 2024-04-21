using System.ComponentModel.DataAnnotations;

namespace TextApp.Dtos.GroupDtos
{
    public class CreateGroupDto
    {
        [Required]
        public Guid[] Members { get; set; }
    }
}
