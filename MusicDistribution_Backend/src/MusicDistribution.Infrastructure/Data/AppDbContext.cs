using Microsoft.EntityFrameworkCore;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Dsp> Dsps => Set<Dsp>();
    public DbSet<TrackDistribution> TrackDistributions => Set<TrackDistribution>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "Nour El Din", Email = "nour.eldin@example.com", Country = "Egypt" },
            new Artist { Id = 2, Name = "Maya Hassan", Email = "maya.hassan@example.com", Country = "Lebanon" },
            new Artist { Id = 3, Name = "Omar Farouk", Email = "omar.farouk@example.com", Country = "Jordan" });

        modelBuilder.Entity<Dsp>().HasData(
            new Dsp { Id = 1, Name = "Spotify" },
            new Dsp { Id = 2, Name = "Apple Music" },
            new Dsp { Id = 3, Name = "YouTube" });

        modelBuilder.Entity<Track>().HasData(
            new Track { Id = 1, Title = "Cairo at Dawn", ArtistId = 1, ISRC = "EGABC2600001", ReleaseDate = new DateOnly(2024, 2, 16), Genre = "Arabic Pop", Status = Domain.Enums.TrackStatus.Distributed },
            new Track { Id = 2, Title = "Nile Lights", ArtistId = 1, ISRC = "EGABC2600002", ReleaseDate = new DateOnly(2023, 9, 8), Genre = "Indie Pop", Status = Domain.Enums.TrackStatus.Submitted },
            new Track { Id = 3, Title = "Beirut Blue", ArtistId = 2, ISRC = "LBDEF2600001", ReleaseDate = new DateOnly(2022, 6, 3), Genre = "Alternative", Status = Domain.Enums.TrackStatus.Distributed },
            new Track { Id = 4, Title = "Paper Moon", ArtistId = 2, ISRC = "LBDEF2600002", ReleaseDate = new DateOnly(2024, 11, 22), Genre = "Acoustic", Status = Domain.Enums.TrackStatus.Draft },
            new Track { Id = 5, Title = "Desert Radio", ArtistId = 3, ISRC = "JOXYZ2600001", ReleaseDate = new DateOnly(2021, 4, 14), Genre = "Electronic", Status = Domain.Enums.TrackStatus.Distributed },
            new Track { Id = 6, Title = "Amman Nights", ArtistId = 3, ISRC = "JOXYZ2600002", ReleaseDate = new DateOnly(2025, 1, 10), Genre = "R&B", Status = Domain.Enums.TrackStatus.Submitted },
            new Track { Id = 7, Title = "Olive Branches", ArtistId = 2, ISRC = "LBDEF2600003", ReleaseDate = new DateOnly(2020, 12, 1), Genre = "Folk", Status = Domain.Enums.TrackStatus.Draft },
            new Track { Id = 8, Title = "Open Skies", ArtistId = 1, ISRC = "EGABC2600003", ReleaseDate = new DateOnly(2023, 3, 27), Genre = "Jazz", Status = Domain.Enums.TrackStatus.Distributed });

        modelBuilder.Entity<TrackDistribution>().HasData(
            new TrackDistribution { Id = 1, TrackId = 1, DspId = 1, SubmittedAt = new DateTimeOffset(2024, 1, 20, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Live },
            new TrackDistribution { Id = 2, TrackId = 1, DspId = 2, SubmittedAt = new DateTimeOffset(2024, 1, 21, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Live },
            new TrackDistribution { Id = 3, TrackId = 1, DspId = 3, SubmittedAt = new DateTimeOffset(2024, 1, 22, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Pending },
            new TrackDistribution { Id = 4, TrackId = 2, DspId = 1, SubmittedAt = new DateTimeOffset(2023, 9, 1, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Pending },
            new TrackDistribution { Id = 5, TrackId = 3, DspId = 2, SubmittedAt = new DateTimeOffset(2022, 5, 20, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Live },
            new TrackDistribution { Id = 6, TrackId = 4, DspId = 1, SubmittedAt = new DateTimeOffset(2024, 11, 15, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Rejected },
            new TrackDistribution { Id = 7, TrackId = 5, DspId = 3, SubmittedAt = new DateTimeOffset(2021, 4, 1, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Live },
            new TrackDistribution { Id = 8, TrackId = 6, DspId = 2, SubmittedAt = new DateTimeOffset(2025, 1, 5, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Pending },
            new TrackDistribution { Id = 9, TrackId = 8, DspId = 1, SubmittedAt = new DateTimeOffset(2023, 3, 20, 10, 0, 0, TimeSpan.Zero), Status = Domain.Enums.DistributionStatus.Live });
    }
}
