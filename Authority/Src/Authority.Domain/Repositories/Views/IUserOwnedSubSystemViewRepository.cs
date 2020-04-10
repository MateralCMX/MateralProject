using Authority.Domain.Views;
using Materal.TTA.EFRepository;

namespace Authority.Domain.Repositories.Views
{
    /// <summary>
    /// 用户拥有的子系统仓储
    /// </summary>
    public interface IUserOwnedSubSystemViewRepository : IEFRepository<UserOwnedSubSystemView, int>
    {
    }
}
