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
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(
                    u => u.Role,
                    opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : "")
                );
        }
    }
}
