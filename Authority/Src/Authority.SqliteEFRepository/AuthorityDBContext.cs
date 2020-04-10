using Authority.Domain;
using Authority.Domain.Views;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authority.SqliteEFRepository
{
    /// <summary>
    /// ProtalServer数据库上下文
    /// </summary>
    public sealed class AuthorityDBContext : DbContext
    {
        public AuthorityDBContext(DbContextOptions<AuthorityDBContext> options) : base(options)
        {
        }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Role { get; set; }
        /// <summary>
        /// 角色菜单映射
        /// </summary>
        public DbSet<RoleWebMenuMap> RoleWebMenuMap { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public DbSet<SubSystem> SubSystem { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 用户角色映射
        /// </summary>
        public DbSet<UserRoleMap> UserRoleMap { get; set; }
        /// <summary>
        /// 网页菜单
        /// </summary>
        public DbSet<WebMenu> WebMenu { get; set; }
        /// <summary>
        /// 用户拥有的子系统
        /// </summary>
        public DbSet<UserOwnedSubSystemView> UserOwnedSubSystemView { get; set; }
        /// <summary>
        /// 用户拥有的网页菜单
        /// </summary>
        public DbSet<UserOwnedWebMenuView> UserOwnedWebMenuView { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
