using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.DotNetty.ControllerBus;
using MateralProject.Core;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace MateralProject.Controllers
{
    public abstract class MateralProjectBaseController : BaseController
    {
        private Guid? loginUserID;
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            try
            {
                ICharSequence authorization = Request.Headers.Get(HttpHeaderNames.Authorization, null);
                if (authorization == null) throw new MateralProjectException("未识别Token");
                string token = authorization.ToString();
                string[] tokens = token.Split(' ');
                if (tokens.Length != 2 || tokens[0] != "Bearer") throw new MateralProjectException("未识别Token");
                return tokens[1];
            }
            catch (Exception ex)
            {
                throw new MateralProjectException("未识别Token", ex);
            }
        }
        /// <summary>
        /// 获得JWTToken对象
        /// </summary>
        /// <returns></returns>
        public JwtSecurityToken GetJwtSecurityToken()
        {
            string token = GetToken();
            var jwtSecurityToken = new JwtSecurityToken(token);
            if (!jwtSecurityToken.Audiences.Contains("WebAPI")) throw new MateralProjectException("未识别Token");
            if (!jwtSecurityToken.Issuer.Equals("Materal.ConfigCenter")) throw new MateralProjectException("未识别Token");
            if (jwtSecurityToken.ValidTo < DateTime.UtcNow) throw new MateralProjectException("未识别Token");
            return jwtSecurityToken;
        }
        /// <summary>
        /// 获取登录用户唯一标识
        /// </summary>
        /// <returns></returns>
        public Guid GetLoginUserID()
        {
            try
            {
                if (loginUserID != null) return loginUserID.Value;
                JwtSecurityToken jwtSecurityToken = GetJwtSecurityToken();
                if (!jwtSecurityToken.Payload.TryGetValue("UserID", out object value)) throw new MateralProjectException("未识别Token");
                loginUserID = Guid.Parse(value.ToString());
                return loginUserID.Value;
            }
            catch (MateralProjectException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MateralProjectException("未识别Token", ex);
            }
        }
    }
}
