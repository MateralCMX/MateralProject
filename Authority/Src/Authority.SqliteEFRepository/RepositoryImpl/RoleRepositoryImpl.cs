using System;
using Authority.Domain;
using Authority.Domain.Repositories;

namespace Authority.SqliteEFRepository.RepositoryImpl
{
    public class RoleRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<Role, Guid>, IRoleRepository
    {
        public RoleRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
