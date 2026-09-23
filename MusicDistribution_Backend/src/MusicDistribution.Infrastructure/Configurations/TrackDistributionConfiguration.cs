using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Infrastructure.Configurations;

public class TrackDistributionConfiguration : IEntityTypeConfiguration<TrackDistribution>
{
    public void Configure(EntityTypeBuilder<TrackDistribution> builder)
    {
        builder.HasKey(distribution => distribution.Id);
        builder.Property(distribution => distribution.SubmittedAt).IsRequired();
        builder.Property(distribution => distribution.Status).IsRequired().HasConversion<int>();
        builder.HasIndex(distribution => new { distribution.TrackId, distribution.DspId }).IsUnique();

        builder.HasOne(distribution => distribution.Track)
            .WithMany(track => track.TrackDistributions)
            .HasForeignKey(distribution => distribution.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(distribution => distribution.Dsp)
            .WithMany(dsp => dsp.TrackDistributions)
            .HasForeignKey(distribution => distribution.DspId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
