using AspectCore.DynamicProxy;
using Authority.Common;
using Authority.DataTransmitModel.User;
using Authority.PresentationModel.User;
using Authority.Service;
using Authority.Service.Models.User;
using Materal.ConfigCenter;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using MateralProject.Controllers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Authority.Controllers
{
    public class UserController : MateralProjectBaseController
    {
        private readonly IUserService _userService;
        private readonly ApplicationConfig _applicationConfig;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, ApplicationConfig applicationConfig, IMapper mapper)
        {
            _userService = userService;
            _applicationConfig = applicationConfig;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddUser(AddUserRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddUserModel>(requestModel);
                await _userService.AddUserAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditUser(EditUserRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditUserModel>(requestModel);
                await _userService.EditUserAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetUserInfo(Guid id)
        {
            try
            {
                UserDTO result = await _userService.GetUserInfoAsync(id);
                return ResultModel<UserDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfo()
        {
            try
            {
                UserDTO result = await _userService.GetUserInfoAsync(GetLoginUserID());
                return ResultModel<UserDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<UserListDTO>> GetUserList(QueryUserFilterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<QueryUserFilterModel>(requestModel);
                (List<UserListDTO> result, PageModel pageModel) = await _userService.GetUserListAsync(model);
                return PageResultModel<UserListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return PageResultModel<UserListDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return PageResultModel<UserListDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAuthority]
        public async Task<ResultModel<TokenResultModel>> Login(LoginRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<LoginModel>(requestModel);
                UserDTO user = await _userService.LoginAsync(model);
                string token = GetToken(user);
                var result = new TokenResultModel
                {
                    ExpiresSecond = _applicationConfig.JWTConfig.ExpiredTime,
                    AccessToken = token
                };
                return ResultModel<TokenResultModel>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<TokenResultModel>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<TokenResultModel>.Fail(ex.Message);
            }
        }
        #region 私有方法
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetToken(UserListDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_applicationConfig.JWTConfig.Key);
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(_applicationConfig.JWTConfig.ExpiredTime);
            var securityKey = new SymmetricSecurityKey(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Aud,_applicationConfig.JWTConfig.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss,_applicationConfig.JWTConfig.Issuer),
                    new Claim("UserID",user.ID.ToString())
                }),
                Audience = _applicationConfig.JWTConfig.Audience,
                Issuer = _applicationConfig.JWTConfig.Issuer,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        #endregion
    }
}
