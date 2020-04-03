using Materal.TTA.SqliteRepository;

namespace Authority.SqliteEFRepository
{
    public class AuthoritySqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<AuthorityDBContext>, IAuthorityUnitOfWork
    {
        public AuthoritySqliteEFUnitOfWorkImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
