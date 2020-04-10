using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.SqliteEFRepository.ModelConfig
{
    internal sealed class UserRoleMapConfig : IEntityTypeConfiguration<UserRoleMap>
    {
        public void Configure(EntityTypeBuilder<UserRoleMap> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.UserID)
                .IsRequired();
            builder.Property(e => e.RoleID)
                .IsRequired();
        }
    }
}
