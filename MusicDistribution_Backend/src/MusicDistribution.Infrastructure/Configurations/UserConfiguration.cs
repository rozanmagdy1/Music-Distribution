using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Email).IsRequired().HasMaxLength(254);
        builder.Property(user => user.PasswordHash).IsRequired().HasMaxLength(512);
        builder.HasIndex(user => user.Email).IsUnique();
    }
}
