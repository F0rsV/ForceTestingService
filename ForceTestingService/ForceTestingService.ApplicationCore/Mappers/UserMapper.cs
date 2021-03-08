using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class UserMapper: GenericMapper<User, UserDto>
    {
        public override User Map(UserDto dto)
        {
            throw new System.NotImplementedException();
        }

        public override UserDto Map(User entity)
        {
            return new UserDto()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}