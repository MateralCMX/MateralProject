using Materal.TTA.EFRepository;
using System;

namespace Authority.Domain.Repositories
{
    public interface IWebMenuRepository : IEFRepository<WebMenu, Guid>
    {
        /// <summary>
        /// 获得最大位序
        /// </summary>
        /// <returns></returns>
        int GetMaxIndex();
    }
}
