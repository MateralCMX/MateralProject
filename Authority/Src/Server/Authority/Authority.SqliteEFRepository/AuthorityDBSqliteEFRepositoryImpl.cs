using System;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Authority.SqliteEFRepository
{
    public class AuthorityDBSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : class, IEntity<Guid>, new()
    {
        public AuthorityDBSqliteEFRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
