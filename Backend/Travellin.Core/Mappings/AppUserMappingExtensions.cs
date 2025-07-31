using Travellin.Core.Dtos.Properties;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class AppUserMappingExtensions
    {
        public static PropertyOwnerDto ToDto(this AppUser user)
        {
            if (user == null)
                return null;
            return new PropertyOwnerDto
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
