using System;
using Authority.Domain;
using Authority.Domain.Repositories;

namespace Authority.SqliteEFRepository.RepositoryImpl
{
    public class UserRoleMapRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<UserRoleMap, Guid>, IUserRoleMapRepository
    {
        public UserRoleMapRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
