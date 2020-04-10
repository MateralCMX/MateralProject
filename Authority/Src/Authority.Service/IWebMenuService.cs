using Authority.DataTransmitModel.WebMenu;
using Authority.Service.Models.WebMenu;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Service
{
    /// <summary>
    /// 网页菜单权限服务
    /// </summary>
    public interface IWebMenuService
    {
        /// <summary>
        /// 添加网页菜单权限
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task AddWebMenuAsync(AddWebMenuModel model);
        /// <summary>
        /// 修改网页菜单权限
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task EditWebMenuAsync(EditWebMenuModel model);
        /// <summary>
        /// 删除网页菜单权限
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task DeleteWebMenuAsync(Guid id);
        /// <summary>
        /// 获得网页菜单权限信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task<WebMenuDTO> GetWebMenuInfoAsync(Guid id);
        /// <summary>
        /// 获得网页菜单权限树形
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task<List<WebMenuTreeDTO>> GetWebMenuTreeAsync(Guid subSystemID);
        /// <summary>
        /// 获得用户拥有的网页菜单权限树形
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task<List<WebMenuTreeDTO>> GetWebMenuTreeAsync(Guid userID, Guid subSystemID);
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID">唯一标识</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <param name="targetID">位序目标唯一标识</param>
        /// <param name="forUnder">在位序目标之下</param>
        /// <returns></returns>
        [DataValidation]
        Task ExchangeWebMenuIndexAsync(Guid exchangeID, Guid? parentID, Guid? targetID, bool forUnder = true);
    }
}
