using System;
using System.Reflection;
using System.Threading.Tasks;
using Authority.Common;
using Authority.SqliteEFRepository;
using Materal.DotNetty.Server.Core;
using Materal.DotNetty.Server.CoreImpl;
using MateralProject.Consul;
using MateralProject.Core;
using MateralProject.EFHelper;

namespace Authority.Server
{
    public class Program
    {
        private static ConsulHelper _consulHelper;
        private static IDotNettyServer _dotNettyServer;
        public static async Task Main()
        {
            string version = Assembly.Load("Authority.Server").GetName().Version.ToString();
            Console.Title = $"MateralProject-AuthorityServer [版本号:{version}]";
            ConsoleHelper.ServerName = "AuthorityServer";
            if (TryRegisterService())
            {
                try
                {
                    var applicationConfig = ApplicationService.GetService<ApplicationConfig>();
                    DataBaseHandler();
                    await DotNettyServerHandlerAsync(applicationConfig);
                    await ConsulHandlerAsync(applicationConfig);
                    await StopAsync();
                }
                catch (Exception exception)
                {
                    ConsoleHelper.ServerWriteLine("服务器发生致命错误", "错误", ConsoleColor.Red);
                    ConsoleHelper.ServerWriteError(exception);
                }
            }
            else
            {
                ConsoleHelper.ServerWriteLine("注册服务失败", "失败", ConsoleColor.Red);
            }
        }
        #region 私有方法
        /// <summary>
        /// 结束
        /// </summary>
        /// <returns></returns>
        private static async Task StopAsync()
        {
            ConsoleHelper.ServerWriteLine("输入Stop停止服务");
            string inputKey = string.Empty;
            while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
            {
                inputKey = Console.ReadLine();
                if (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                {
                    ConsoleHelper.ServerWriteError(new Exception("未识别命令请重新输入"));
                }
            }
            _consulHelper.UnRegisterConsul();
            await _dotNettyServer.StopAsync();
        }
        /// <summary>
        /// 数据库处理器
        /// </summary>
        private static void DataBaseHandler()
        {
            var contextHelper = ApplicationService.GetService<DBContextHelper<AuthorityDBContext>>();
            ConsoleHelper.ServerWriteLine("数据库初始化......");
            contextHelper.Migrate();
        }
        /// <summary>
        /// DotNetty服务处理器
        /// </summary>
        /// <param name="applicationConfig"></param>
        /// <returns></returns>
        private static async Task DotNettyServerHandlerAsync(ApplicationConfig applicationConfig)
        {
            _dotNettyServer = ApplicationService.GetService<IDotNettyServer>();
            _dotNettyServer.OnConfigHandler += DotNettyServer_OnConfigHandler;
            _dotNettyServer.OnException += ConsoleHelper.ServerWriteError;
            _dotNettyServer.OnGetCommand += Console.ReadLine;
            _dotNettyServer.OnMessage += message => ConsoleHelper.ServerWriteLine(message);
            _dotNettyServer.OnSubMessage += (message, subTitle) => ConsoleHelper.ServerWriteLine(message, subTitle);
            await _dotNettyServer.RunAsync(applicationConfig.ServerConfig);
            ConsoleHelper.ServerWriteLine(
                $"已监听http://{applicationConfig.ServerConfig.Host}:{applicationConfig.ServerConfig.Port}/api");
            ConsoleHelper.ServerWriteLine(
                $"已监听http://{applicationConfig.ServerConfig.Host}:{applicationConfig.ServerConfig.Port}");
        }
        /// <summary>
        /// DotNetty服务
        /// </summary>
        /// <param name="channelHandler"></param>
        private static void DotNettyServer_OnConfigHandler(IServerChannelHandler channelHandler)
        {
            channelHandler.AddLastHandler(ApplicationService.GetService<WebAPIHandler>());
            channelHandler.AddLastHandler(ApplicationService.GetService<FileHandler>());
        }
        /// <summary>
        /// Consul处理器
        /// </summary>
        /// <param name="applicationConfig"></param>
        /// <returns></returns>
        private static async Task ConsulHandlerAsync(ApplicationConfig applicationConfig)
        {
            _consulHelper = new ConsulHelper(applicationConfig.ConsulConfig, applicationConfig.ServiceConfig);
            _consulHelper.OnMessage += message => ConsoleHelper.ServerWriteLine(message);
            _consulHelper.OnException += ConsoleHelper.ServerWriteError;
            await _consulHelper.RegisterConsulAsync();
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <returns></returns>
        private static bool TryRegisterService()
        {
            try
            {
                ApplicationService.RegisterServices(ServerDIExtension.AddServer, MateralDotNettyServerCoreDIExtension.AddMateralDotNettyServerCore);
                ApplicationService.BuildServices();
                return true;
            }
            catch (Exception exception)
            {
                ConsoleHelper.ServerWriteError(exception);
                return false;
            }
        }
        #endregion
    }
}
