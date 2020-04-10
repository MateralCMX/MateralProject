using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Authority.SqliteEFRepository
{
    public class AuthorityDBSqliteEFRepositoryImpl<T, T2> : SqliteEFRepositoryImpl<T, T2> where T : class, IEntity<T2>, new()
    {
        public AuthorityDBSqliteEFRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
