using System;
using Authority.Domain;
using Authority.Domain.Repositories;

namespace Authority.SqliteEFRepository.RepositoryImpl
{
    public class RoleWebMenuMapRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<RoleWebMenuMap, Guid>, IRoleWebMenuMapRepository
    {
        public RoleWebMenuMapRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
