using Materal.TTA.EFRepository;
using System;

namespace Authority.Domain.Repositories
{
    public interface IUserRoleMapRepository : IEFRepository<UserRoleMap, Guid>
    {
    }
}
