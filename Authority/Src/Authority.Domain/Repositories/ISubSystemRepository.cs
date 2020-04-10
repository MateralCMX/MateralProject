using Materal.TTA.EFRepository;
using System;

namespace Authority.Domain.Repositories
{
    public interface ISubSystemRepository : IEFRepository<SubSystem, Guid>
    {
        /// <summary>
        /// 获得最大位序
        /// </summary>
        /// <returns></returns>
        int GetMaxIndex();
    }
}
