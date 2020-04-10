using Materal.TTA.EFRepository;
using System;

namespace Authority.Domain.Repositories
{
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
