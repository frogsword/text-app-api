using TextApp.Models;

namespace TextApp.Interfaces
{
    public interface IProfileInterface
    {
        Task<Profile> CreateAsync(Profile profileModel);
    }
}
