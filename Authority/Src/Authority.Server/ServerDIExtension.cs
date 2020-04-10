using System.Reflection;
using Authority.Common;
using Authority.DependencyInjection;
using Materal.Common;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.Server.CoreImpl;
using MateralProject.Controllers.Filters;
using MateralProject.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Authority.Server
{
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServer(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
            FileHandler.HtmlPageFolderPath = "HtmlPages";
            services.AddMemoryCache();
            var applicationConfig = new ApplicationConfig();
            services.AddSingleton(applicationConfig);
            services.AddAuthorityServices(applicationConfig.SqliteConfig.ConnectionString);
            services.AddTransient<WebAPIHandler>();
            services.AddTransient<FileHandler>();
            services.AddControllerBus(controllerHelper =>
            {
                controllerHelper.AddFilter<ExceptionFilter>();
                controllerHelper.AddFilter<AuthorityFilterAttribute>();
            }, Assembly.Load("Authority.Controllers"));
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"),
                Assembly.Load("Authority.Controllers"));
        }
    }
}
