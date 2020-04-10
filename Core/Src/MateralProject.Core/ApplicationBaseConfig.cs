using Materal.ConfigCenter.Client;
using Materal.ConvertHelper;
using MateralProject.Core.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace MateralProject.Core
{
    public class ApplicationBaseConfig
    {
        protected IConfiguration _configurationFile;
        protected IConfiguration _configurationCenter;
        public ApplicationBaseConfig()
        {
            _configurationFile = GetConfigurationByConfigurationFile();
            _configurationCenter = GetConfigurationByConfigCenter();
        }
        /// <summary>
        /// 通过配置中心获取配置
        /// </summary>
        private IConfiguration GetConfigurationByConfigCenter()
        {
            return null;
        }
        /// <summary>
        /// 通过配置文件获取配置
        /// </summary>
        private IConfiguration GetConfigurationByConfigurationFile(string fileName = "appsetting")
        {
#if DEBUG
            string appConfigFile = $"{fileName}.Development.json";
#else
            string appConfigFile = $"{fileName}.json";
#endif
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            return builder.Build();
        }
        private JWTConfigModel _jwtConfig;
        /// <summary>
        /// JWT配置
        /// </summary>
        public JWTConfigModel JWTConfig
        {
            get
            {
                if (_jwtConfig != null) return _jwtConfig;
                _jwtConfig = _configurationCenter?.GetValueObject<JWTConfigModel>("JWT") ??
                                new JWTConfigModel
                                {
                                    Key = _configurationFile["JWT:Key"],
                                    Audience = _configurationFile["JWT:Audience"],
                                    Issuer = _configurationFile["JWT:Issuer"],
                                    ExpiredTime = _configurationFile["JWT:ExpiredTime"].ConvertTo<uint>()
                                };
                return _jwtConfig;
            }
        }
        private ConsulConfigModel _consulConfig;
        public ConsulConfigModel ConsulConfig
        {
            get
            {
                if (_consulConfig != null) return _consulConfig;
                _consulConfig = _configurationCenter?.GetValueObject<ConsulConfigModel>("Consul") ??
                    new ConsulConfigModel
                    {
                        Enable = Convert.ToBoolean(_configurationFile["Consul:Enable"]),
                        ConsulUrl = _configurationFile["Consul:ConsulUrl"],
                        ConsulPort = Convert.ToInt32(_configurationFile["Consul:ConsulPort"]),
                    };
                return _consulConfig;
            }
        }
    }
}
