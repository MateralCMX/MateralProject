using Authority.PresentationModel.User;
using Authority.Service.Modelss.User;
using AutoMapper;

namespace Authority.Controllers.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<LoginRequestModel, LoginModel>();
            CreateMap<QueryUserFilterRequestModel, QueryUserFilterModel>();
        }
    }
}
