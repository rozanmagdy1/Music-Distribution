using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Infrastructure.Configurations;

public class DspConfiguration : IEntityTypeConfiguration<Dsp>
{
    public void Configure(EntityTypeBuilder<Dsp> builder)
    {
        builder.HasKey(dsp => dsp.Id);
        builder.Property(dsp => dsp.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(dsp => dsp.Name).IsUnique();
    }
}
