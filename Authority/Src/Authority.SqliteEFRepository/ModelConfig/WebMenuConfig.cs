using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.SqliteEFRepository.ModelConfig
{
    internal sealed class WebMenuConfig : IEntityTypeConfiguration<WebMenu>
    {
        public void Configure(EntityTypeBuilder<WebMenu> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Style)
                .IsRequired(false);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Data)
                .IsRequired(false);
            builder.Property(e => e.Index)
                .IsRequired();
            builder.Property(e => e.ParentID)
                .IsRequired(false);
            builder.Property(e => e.SubSystemID)
                .IsRequired();
        }
    }
}
