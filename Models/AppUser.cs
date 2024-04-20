using Microsoft.AspNetCore.Identity;

namespace TextApp.Models
{
    public class AppUser : IdentityUser
    {
        public Profile Profile { get; set; }
    }
}
