using Microsoft.EntityFrameworkCore;

namespace TextApp.Models
{
    public class Profile
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public byte[]? Picture { get; set; }
        public Guid[]? Contacts { get; set; }
        public Guid[]? Blocks { get; set; }
        public AppUser User { get; set; }
    }
}
