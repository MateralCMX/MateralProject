using AspectCore.DynamicProxy;
using AutoMapper;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using MateralProject.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Authority.Common;
using Authority.DataTransmitModel.WebMenu;
using Authority.PresentationModel.WebMenu;
using Authority.Service;
using Authority.Service.Models.WebMenu;
using MateralProject.Core.Models;

namespace Authority.Controllers
{
    public class WebMenuController : MateralProjectBaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebMenuService _webMenuService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public WebMenuController(IWebMenuService webMenuService, IMapper mapper, ApplicationConfig applicationConfig) : base(applicationConfig)
        {
            _webMenuService = webMenuService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加网页菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddWebMenu(AddWebMenuRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddWebMenuModel>(requestModel);
                await _webMenuService.AddWebMenuAsync(model);
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
        /// 修改网页菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditWebMenu(EditWebMenuRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditWebMenuModel>(requestModel);
                await _webMenuService.EditWebMenuAsync(model);
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
        /// 删除网页菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteWebMenu([Required(ErrorMessage = "唯一标识不可以为空")]Guid id)
        {
            try
            {
                await _webMenuService.DeleteWebMenuAsync(id);
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
        /// 获得网页菜单权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<WebMenuDTO>> GetWebMenuInfo([Required(ErrorMessage = "唯一标识不可以为空")]Guid id)
        {
            try
            {
                WebMenuDTO result = await _webMenuService.GetWebMenuInfoAsync(id);
                return ResultModel<WebMenuDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<WebMenuDTO>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<WebMenuDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 调换网页菜单权限位序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ExchangeWebMenuIndex(ExchangeParentIDRequestModel<Guid> requestModel)
        {
            try
            {
                await _webMenuService.ExchangeWebMenuIndexAsync(requestModel.ExchangeID, requestModel.ParentID, requestModel.TargetID, requestModel.ForUnder);
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
        /// 获得网页菜单权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<WebMenuTreeDTO>>> GetWebMenuTree([Required(ErrorMessage = "子系统唯一标识不可以为空")]Guid subSystemID)
        {
            try
            {
                List<WebMenuTreeDTO> result = await _webMenuService.GetWebMenuTreeAsync(subSystemID);
                return ResultModel<List<WebMenuTreeDTO>>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<List<WebMenuTreeDTO>>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<WebMenuTreeDTO>>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得用户拥有的网页菜单权限树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<WebMenuTreeDTO>>> GetUserHasWebMenuTree(GetUserHasWebMenuTreeRequestModel requestModel)
        {
            try
            {
                Guid userID = GetLoginUserID();
                List<WebMenuTreeDTO> result =
                    await _webMenuService.GetWebMenuTreeAsync(userID, requestModel.SubSystemID);
                return ResultModel<List<WebMenuTreeDTO>>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<List<WebMenuTreeDTO>>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<WebMenuTreeDTO>>.Fail(ex.Message);
            }
        }
    }
}
