using Materal.TTA.EFRepository;
using System;

namespace Authority.Domain.Repositories
{
    public interface IRoleRepository : IEFRepository<Role, Guid>
    {
    }
}
