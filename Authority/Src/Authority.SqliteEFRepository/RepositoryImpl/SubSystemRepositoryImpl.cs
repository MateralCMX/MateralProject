using System;
using System.Linq;
using Authority.Domain;
using Authority.Domain.Repositories;

namespace Authority.SqliteEFRepository.RepositoryImpl
{
    public class SubSystemRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<SubSystem, Guid>, ISubSystemRepository
    {
        public SubSystemRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
        public int GetMaxIndex()
        {
            return DBSet.Any() ? DBSet.Max(m => m.Index) : -1;
        }
    }
}
