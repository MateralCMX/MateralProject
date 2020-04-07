using System;
using DotNetty.Codecs.Http;
using Materal.ConfigCenter;
using Materal.DotNetty.Common;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Filters;
using MateralProject.Core;

namespace MateralProject.Controllers.Filters
{
    public class AuthorityFilterAttribute : BaseAuthorityFilterAttribute
    {
        public override void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response)
        {
            if (!(baseController is MateralProjectBaseController materalProjectBaseController)) return;
            try
            {
                Guid loginUserID = materalProjectBaseController.GetLoginUserID();
                if (loginUserID == Guid.Empty)
                {
                    response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
                }
            }
            catch (MateralProjectException)
            {
                response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
            }
        }
    }
}
