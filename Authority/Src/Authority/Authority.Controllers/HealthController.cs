using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using MateralProject.Controllers;

namespace Authority.Controllers
{
    [AllowAuthority]
    public class HealthController : MateralProjectBaseController
    {
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
