using System;
using Authority.Domain.Repositories.Views;
using Authority.Domain.Views;

namespace Authority.SqliteEFRepository.RepositoryImpl.Views
{
    /// <summary>
    /// 用户拥有的子系统仓储
    /// </summary>
    public class UserOwnedSubSystemViewRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<UserOwnedSubSystemView, int>, IUserOwnedSubSystemViewRepository
    {
        public UserOwnedSubSystemViewRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
