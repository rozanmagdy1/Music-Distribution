using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Infrastructure.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(artist => artist.Id);
        builder.Property(artist => artist.Name).IsRequired().HasMaxLength(150);
        builder.Property(artist => artist.Email).IsRequired().HasMaxLength(254);
        builder.Property(artist => artist.Country).IsRequired().HasMaxLength(100);

        builder.HasMany(artist => artist.Tracks)
            .WithOne(track => track.Artist)
            .HasForeignKey(track => track.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
