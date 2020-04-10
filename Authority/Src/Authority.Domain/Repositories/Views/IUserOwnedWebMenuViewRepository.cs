using Authority.Domain.Views;
using Materal.TTA.EFRepository;

namespace Authority.Domain.Repositories.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限仓储
    /// </summary>
    public interface IUserOwnedWebMenuViewRepository : IEFRepository<UserOwnedWebMenuView, int>
    {
    }
}
