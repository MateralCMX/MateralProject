using Authority.Common;
using Authority.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Authority.DependencyInjection
{
    /// <summary>
    /// 权限依赖注入扩展类
    /// </summary>
    public static class AuthorityDIExtension
    {
        /// <summary>
        /// 添加权限服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorityServices(this IServiceCollection services)
        {
            services.AddDbContext<AuthorityDBContext>(options => options.UseSqlite(
                ApplicationConfig.SqliteConfig.ConnectionString,
                o => { }));
            services.AddTransient(typeof(IAuthorityUnitOfWork), typeof(AuthorityUnitOfWorkImpl));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Authority.EFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Authority.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
        }
    }
}
