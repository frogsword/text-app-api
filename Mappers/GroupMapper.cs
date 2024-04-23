using TextApp.Dtos.GroupDtos;
using TextApp.Dtos.MessageDtos;
using TextApp.Models;

namespace TextApp.Mappers
{
    public static class GroupMapper
    {
        public static Group ToGroupFromCreateDto(this CreateGroupDto groupDto)
        {
            return new Group
            {
                Members = groupDto.Members,
                Name = groupDto.Name,
            };
        }
    }
}
