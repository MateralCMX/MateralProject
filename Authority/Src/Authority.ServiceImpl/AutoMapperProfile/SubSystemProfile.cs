using Authority.DataTransmitModel.SubSystem;
using Authority.Domain;
using Authority.Domain.Views;
using AutoMapper;

namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class SubSystemProfile : Profile
    {
        public SubSystemProfile()
        {
            CreateMap<SubSystem, SubSystemListDTO>();
            CreateMap<UserOwnedSubSystemView, SubSystemListDTO>()
                .BeforeMap((view, dto) => dto.ID = view.SubSystemID);
            CreateMap<SubSystem, SubSystemDTO>();
        }
    }
}
