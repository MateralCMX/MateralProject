using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.SqliteEFRepository.ModelConfig
{
    internal sealed class SubSystemConfig : IEntityTypeConfiguration<SubSystem>
    {
        public void Configure(EntityTypeBuilder<SubSystem> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Style)
                .IsRequired(false);
            builder.Property(e => e.Index)
                .IsRequired();
            builder.Property(e => e.Display)
                .IsRequired();
            builder.Property(e => e.Data)
                .IsRequired(false);
        }
    }
}
