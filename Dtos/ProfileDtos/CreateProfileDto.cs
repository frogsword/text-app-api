using System.ComponentModel.DataAnnotations;
using TextApp.Models;

namespace TextApp.Dtos.ProfileDtos
{
    public class CreateProfileDto
    {
        [Required]
        [MinLength(1)]
        public string UserId { get; set; }

        [Required]
        [MinLength(1)]
        public string Username { get; set; }

        public byte[]? Picture { get; set; }

        public Guid[] Contacts { get; set; }

        public Guid[] Blocks { get; set; }

        [Required]
        public AppUser User { get; set; }

    }
}
