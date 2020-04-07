using Authority.Domain;
using Authority.Domain.Repositories;

namespace Authority.SqliteEFRepository.RepositoryImpl
{
    public class UserRepositoryImpl : AuthorityDBSqliteEFRepositoryImpl<User>, IUserRepository
    {
        public UserRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
