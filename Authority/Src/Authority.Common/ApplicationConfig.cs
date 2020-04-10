using Materal.ConfigCenter.Client;
using Materal.DotNetty.Server.Core;
using Materal.TTA.SqliteRepository.Model;
using MateralProject.Core;
using MateralProject.Core.Models;
using System;

namespace Authority.Common
{
    public class ApplicationConfig : ApplicationBaseConfig
    {
        #region 配置
        private ServerConfig _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public ServerConfig ServerConfig
        {
            get
            {
                if (_serverConfig != null) return _serverConfig;
                _serverConfig = _configurationCenter?.GetValueObject<ServerConfig>("ServerConfig") ??
                                new ServerConfig
                                {
                                    Host = _configurationFile["ServerConfig:Host"],
                                    Port = Convert.ToInt32(_configurationFile["ServerConfig:Port"])
                                };
                return _serverConfig;
            }
        }
        private SqliteConfigModel _sqliteConfig;
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public SqliteConfigModel SqliteConfig
        {
            get
            {
                if (_sqliteConfig != null) return _sqliteConfig;
                _sqliteConfig = _configurationCenter?.GetValueObject<SqliteConfigModel>("SqliteConfig") ??
                                new SqliteConfigModel
                                {
                                    FilePath = _configurationFile["SqliteConfig:FilePath"],
                                    Password = _configurationFile["SqliteConfig:Password"]
                                };
                return _sqliteConfig;
            }
        }
        private ServiceConfigModel _serviceConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public ServiceConfigModel ServiceConfig
        {
            get
            {
                if (_serviceConfig != null) return _serviceConfig;
                _serviceConfig = _configurationCenter?.GetValueObject<ServiceConfigModel>("ServiceConfig") ??
                                 new ServiceConfigModel
                                 {
                                     ServiceName = _configurationFile["ServiceConfig:ServiceName"],
                                     ServiceHealth = _configurationFile["ServiceConfig:ServiceHealth"]
                                 };
                _serviceConfig.ChangeService($"http://{ServerConfig.Host}:{ServerConfig.Port}");
                return _serviceConfig;
            }
        }
        #endregion
    }
}
