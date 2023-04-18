using AutoMapper;

using Mineshard.Api.Models.DTO.Role;
using Mineshard.Api.Models.DTO.User;
using Mineshard.Persistence.Models.Auth;

namespace Mineshard.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Users Mappings
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserRequest>().ReverseMap();

            // Roles Mappings
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
