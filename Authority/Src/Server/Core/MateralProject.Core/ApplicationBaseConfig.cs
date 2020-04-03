using Microsoft.Extensions.Configuration;
using System;

namespace MateralProject.Core
{
    public class ApplicationBaseConfig
    {
        /// <summary>
        /// 通过配置中心获取配置
        /// </summary>
        public static IConfiguration GetConfigurationByConfigCenter()
        {
            return null;
        }
        /// <summary>
        /// 通过配置文件获取配置
        /// </summary>
        public static IConfiguration GetConfigurationByConfigurationFile(string fileName = "appsetting")
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
    }
}
