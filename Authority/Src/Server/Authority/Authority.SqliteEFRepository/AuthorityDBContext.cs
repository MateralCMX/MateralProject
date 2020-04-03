using Authority.Domain;
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
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
