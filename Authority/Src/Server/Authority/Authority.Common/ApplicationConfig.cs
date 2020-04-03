using System;
using Materal.ConfigCenter.Client;
using Materal.DotNetty.Server.Core;
using Materal.TTA.SqliteRepository.Model;
using MateralProject.Core;
using Microsoft.Extensions.Configuration;

namespace Authority.Common
{
    public static class ApplicationConfig
    {
        #region 配置对象
        private static IConfiguration _configurationFile;
        private static IConfiguration _configurationCenter;
        /// <summary>
        /// 配置生成
        /// </summary>
        public static void ConfigurationBuilder()
        {
            _configurationFile = ApplicationBaseConfig.GetConfigurationByConfigurationFile();
            _configurationCenter = ApplicationBaseConfig.GetConfigurationByConfigCenter();
        }
        #endregion
        #region 配置
        private static ServerConfig _serverConfig;

        /// <summary>
        /// 服务配置
        /// </summary>
        public static ServerConfig ServerConfig
        {
            get
            {
                if (_serverConfig != null) return _serverConfig;
                const string serverConfigName = "ServerConfig";
                _serverConfig = _configurationCenter?.GetValueObject<ServerConfig>(serverConfigName) ??
                                new ServerConfig
                                {
                                    Host = _configurationFile["ServerConfig:Host"],
                                    Port = Convert.ToInt32(_configurationFile["ServerConfig:Port"])
                                };
                return _serverConfig;
            }
        }
        private static SqliteConfigModel _sqliteConfig;
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig
        {
            get
            {
                if (_sqliteConfig != null) return _sqliteConfig;
                const string serverConfigName = "SqliteConfig";
                _sqliteConfig = _configurationCenter?.GetValueObject<SqliteConfigModel>(serverConfigName) ??
                                new SqliteConfigModel
                                {
                                    FilePath = _configurationFile["SqliteConfig:FilePath"],
                                    Password = _configurationFile["SqliteConfig:Password"]
                                };
                return _sqliteConfig;
            }
        }
        #endregion
    }
}
