using AspectCore.DynamicProxy;
using Authority.DataTransmitModel.SubSystem;
using Authority.Service;
using Authority.Service.Models.SubSystem;
using AutoMapper;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using MateralProject.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Authority.Common;
using Authority.PresentationModel.SubSystem;
using MateralProject.Core.Models;

namespace Authority.Controllers
{
    public class SubSystemController : MateralProjectBaseController
    {
        private readonly IMapper _mapper;
        private readonly ISubSystemService _subSystemService;
        /// <summary>
        /// 构造方法
        /// </summary>
        public SubSystemController(ISubSystemService subSystemService, IMapper mapper, ApplicationConfig applicationConfig) : base(applicationConfig)
        {
            _subSystemService = subSystemService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加子系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddSubSystem(AddSubSystemRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddSubSystemModel>(requestModel);
                await _subSystemService.AddSubSystemAsync(model);
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
        /// 修改子系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditSubSystem(EditSubSystemRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditSubSystemModel>(requestModel);
                await _subSystemService.EditSubSystemAsync(model);
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
        /// 删除子系统
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteSubSystem([Required(ErrorMessage = "唯一标识不可以为空")]Guid id)
        {
            try
            {
                await _subSystemService.DeleteSubSystemAsync(id);
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
        /// 获得子系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<SubSystemDTO>> GetSubSystemInfo([Required(ErrorMessage = "唯一标识不可以为空")]Guid id)
        {
            try
            {
                SubSystemDTO result = await _subSystemService.GetSubSystemInfoAsync(id);
                return ResultModel<SubSystemDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<SubSystemDTO>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<SubSystemDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 调换子系统位序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ExchangeSubSystemIndex(ExchangeIndexRequestModel<Guid> requestModel)
        {
            try
            {
                await _subSystemService.ExchangeSubSystemIndexAsync(requestModel.ExchangeID, requestModel.TargetID, requestModel.ForUnder);
                return ResultModel.Success("调换成功");
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
        /// 获得子系统列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<SubSystemListDTO>> GetSubSystemList(QuerySubSystemFilterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<QuerySubSystemFilterModel>(requestModel);
                (List<SubSystemListDTO> result, PageModel pageModel) = await _subSystemService.GetSubSystemListAsync(model);
                return PageResultModel<SubSystemListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return PageResultModel<SubSystemListDTO>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return PageResultModel<SubSystemListDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得用户可见子系统列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<SubSystemListDTO>>> GetUserHasSubSystemList()
        {
            try
            {
                Guid userID = GetLoginUserID();
                List<SubSystemListDTO> result = await _subSystemService.GetUserHasSubSystemListAsync(userID);
                return ResultModel<List<SubSystemListDTO>>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<List<SubSystemListDTO>>.Fail(ex.InnerException?.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<SubSystemListDTO>>.Fail(ex.Message);
            }
        }
    }
}
