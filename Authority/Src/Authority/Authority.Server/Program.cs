using Authority.Common;
using Materal.ConvertHelper;
using Materal.TTA.SqliteRepository.Model;
using System;

namespace Authority.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApplicationConfig.ConfigurationBuilder();
            SqliteConfigModel sqliteConfig = ApplicationConfig.SqliteConfig;
            Console.WriteLine(sqliteConfig.ToJson());
        }
    }
}
