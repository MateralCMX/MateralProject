using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.SqliteEFRepository.ModelConfig
{
    internal sealed class RoleWebMenuMapConfig : IEntityTypeConfiguration<RoleWebMenuMap>
    {
        public void Configure(EntityTypeBuilder<RoleWebMenuMap> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.RoleID)
                .IsRequired();
            builder.Property(e => e.WebMenuID)
                .IsRequired();
        }
    }
}
