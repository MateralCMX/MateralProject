using AspectCore.DynamicProxy;
using Authority.DataTransmitModel.Role;
using Authority.Service;
using Authority.Service.Models.Role;
using AutoMapper;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using MateralProject.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Authority.Common;
using Authority.PresentationModel.Role;
using MateralProject.Core.Models;

namespace Authority.Controllers
{
    public class RoleController : MateralProjectBaseController
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        /// <summary>
        /// 构造方法
        /// </summary>
        public RoleController(IRoleService roleService, IMapper mapper, ApplicationConfig applicationConfig) : base(applicationConfig)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddRole(AddRoleRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddRoleModel>(requestModel);
                await _roleService.AddRoleAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditRole(EditRoleRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditRoleModel>(requestModel);
                await _roleService.EditRoleAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteRole([Required(ErrorMessage = "唯一标识不可以为空")]Guid id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<RoleDTO>> GetRoleInfo([Required(ErrorMessage = "唯一标识不可以为空")]Guid id)
        {
            try
            {
                RoleDTO result = await _roleService.GetRoleInfoAsync(id);
                return ResultModel<RoleDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<RoleDTO>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<RoleDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获得角色权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<RoleTreeDTO>>> GetRoleTree([Required(ErrorMessage = "子系统唯一标识不可以为空")]Guid subSystemID)
        {
            try
            {
                List<RoleTreeDTO> result = await _roleService.GetRoleTreeAsync(subSystemID);
                return ResultModel<List<RoleTreeDTO>>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<List<RoleTreeDTO>>.Fail(ex.InnerException?.Message);
            }
            catch (ArgumentException ex)
            {
                return ResultModel<List<RoleTreeDTO>>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 更换角色权限父级
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ExchangeRoleParentID(ExchangeParentIDNotIndexIDRequestModel<Guid> requestModel)
        {
            try
            {
                await _roleService.ExchangeRoleParentIDAsync(requestModel.ExchangeID, requestModel.ParentID);
                return ResultModel.Success("修改成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
