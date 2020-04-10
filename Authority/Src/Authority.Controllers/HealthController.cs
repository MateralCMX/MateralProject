using Authority.Common;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using MateralProject.Controllers;
using MateralProject.Core;

namespace Authority.Controllers
{
    [AllowAuthority]
    public class HealthController : MateralProjectBaseController
    {
        public HealthController(ApplicationConfig baseConfig) : base(baseConfig)
        {
        }
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel Inspect()
        {
            return ResultModel.Success("服务运行中");
        }
    }
}
