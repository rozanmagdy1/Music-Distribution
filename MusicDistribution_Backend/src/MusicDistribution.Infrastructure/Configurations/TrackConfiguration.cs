using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Infrastructure.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(track => track.Id);
        builder.Property(track => track.Title).IsRequired().HasMaxLength(200);
        builder.Property(track => track.ISRC).IsRequired().HasMaxLength(12);
        builder.HasIndex(track => track.ISRC).IsUnique();
        builder.Property(track => track.Genre).IsRequired().HasMaxLength(100);
        builder.Property(track => track.Status).IsRequired().HasConversion<int>();
    }
}
