using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TextApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TextApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {        
        }
        public DbSet<Message> Messages { get; set; }
    }
}
