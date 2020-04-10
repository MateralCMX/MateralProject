using Authority.DataTransmitModel.SubSystem;
using Authority.Service.Models.SubSystem;
using Materal.Common;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Service
{
    /// <summary>
    /// 子系统服务
    /// </summary>
    public interface ISubSystemService
    {
        /// <summary>
        /// 添加子系统
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task AddSubSystemAsync(AddSubSystemModel model);
        /// <summary>
        /// 修改子系统
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task EditSubSystemAsync(EditSubSystemModel model);
        /// <summary>
        /// 删除子系统
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task DeleteSubSystemAsync(Guid id);
        /// <summary>
        /// 获得子系统信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task<SubSystemDTO> GetSubSystemInfoAsync(Guid id);
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="targetID"></param>
        /// <param name="forUnder"></param>
        /// <returns></returns>
        [DataValidation]
        Task ExchangeSubSystemIndexAsync(Guid exchangeID, Guid targetID, bool forUnder = true);
        /// <summary>
        /// 获得子系统列表
        /// </summary>
        /// <param name="filterModel">查询条件模型</param>
        /// <returns></returns>
        [DataValidation]
        Task<(List<SubSystemListDTO> result, PageModel pageModel)> GetSubSystemListAsync(QuerySubSystemFilterModel filterModel);
        /// <summary>
        /// 获得用户可见子系统列表
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <returns></returns>
        [DataValidation]
        Task<List<SubSystemListDTO>> GetUserHasSubSystemListAsync(Guid userID);
    }
}
