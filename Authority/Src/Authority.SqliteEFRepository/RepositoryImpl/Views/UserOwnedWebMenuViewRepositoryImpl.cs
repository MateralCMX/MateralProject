using Authority.Domain.Repositories.Views;
using Authority.Domain.Views;

namespace Authority.SqliteEFRepository.RepositoryImpl.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限仓储
    /// </summary>
    public class UserOwnedWebMenuViewRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<UserOwnedWebMenuView, int>, IUserOwnedWebMenuViewRepository
    {
        public UserOwnedWebMenuViewRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
