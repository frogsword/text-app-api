using TextApp.Models;

namespace TextApp.Dtos.GroupDtos
{
    public class GroupProfiles
    {
        public string Name { get; set; }

        public Profile[] Profiles { get; set; }
    }
}
