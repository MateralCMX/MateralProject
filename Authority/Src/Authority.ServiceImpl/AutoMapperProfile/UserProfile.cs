using Authority.DataTransmitModel.User;
using Authority.Domain;
using AutoMapper;

namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
