using AutoMapper;
using Authority.DataTransmitModel.WebMenu;
using Authority.Domain;
using Authority.Domain.Views;

namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class WebMenuProfile : Profile
    {
        public WebMenuProfile()
        {
            CreateMap<WebMenu, WebMenuTreeDTO>();
            CreateMap<UserOwnedWebMenuView, WebMenuTreeDTO>()
                .BeforeMap((view, dto) => dto.ID = view.WebMenuID);
            CreateMap<WebMenu, WebMenuDTO>();
        }
    }
}
