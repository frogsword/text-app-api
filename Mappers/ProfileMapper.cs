using TextApp.Dtos.ProfileDtos;
using TextApp.Models;

namespace TextApp.Mappers
{
    public static class ProfileMapper
    {
        public static Profile ToProfileFromCreateDto(this CreateProfileDto profileDto)
        {
            return new Profile
            {
                UserId = profileDto.UserId,
                Username = profileDto.Username,
                Picture = profileDto.Picture,
                Contacts = profileDto.Contacts,
                Blocks = profileDto.Blocks,
                Groups = profileDto.Groups,
                User = profileDto.User,
            };
        }
    }
}
