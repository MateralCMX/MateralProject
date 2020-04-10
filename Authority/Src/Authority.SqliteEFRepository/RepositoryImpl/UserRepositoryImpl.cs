using System;
using Authority.Domain;
using Authority.Domain.Repositories;

namespace Authority.SqliteEFRepository.RepositoryImpl
{
    public class UserRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<User, Guid>, IUserRepository
    {
        public UserRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
