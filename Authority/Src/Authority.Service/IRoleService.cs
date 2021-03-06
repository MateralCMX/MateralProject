﻿using Authority.DataTransmitModel.Role;
using Authority.Service.Models.Role;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Model;

namespace Authority.Service
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task AddRoleAsync(AddRoleModel model);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task EditRoleAsync(EditRoleModel model);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task DeleteRoleAsync(Guid id);
        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task<RoleDTO> GetRoleInfoAsync(Guid id);
        /// <summary>
        /// 获得角色树形
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [DataValidation]
        Task<List<RoleTreeDTO>> GetRoleTreeAsync(Guid subSystemID);
        /// <summary>
        /// 更换父级
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        [DataValidation]
        Task ExchangeRoleParentIDAsync(Guid id, Guid? parentID);
    }
}
