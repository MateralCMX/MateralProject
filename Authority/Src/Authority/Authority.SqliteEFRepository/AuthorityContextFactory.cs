using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Authority.SqliteEFRepository
{
    public class AuthorityDbContextFactory : IDesignTimeDbContextFactory<AuthorityDBContext>
    {
        public AuthorityDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthorityDBContext>();
            var config = new SqliteConfigModel
            {
                FilePath = "AuthorityDB.db",
                Password = "MateralProject",
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new AuthorityDBContext(optionsBuilder.Options);
        }
    }
}
