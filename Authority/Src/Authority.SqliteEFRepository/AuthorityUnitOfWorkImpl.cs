using Materal.TTA.SqliteRepository;

namespace Authority.SqliteEFRepository
{
    public class AuthorityUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<AuthorityDBContext>, IAuthorityUnitOfWork
    {
        public AuthorityUnitOfWorkImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
