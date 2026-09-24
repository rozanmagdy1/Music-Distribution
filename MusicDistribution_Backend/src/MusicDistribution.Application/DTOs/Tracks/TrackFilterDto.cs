using System.ComponentModel.DataAnnotations;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.DTOs.Tracks;

public class TrackFilterDto
{
    [Range(1, int.MaxValue)]
    public int? ArtistId { get; set; }

    [StringLength(100)]
    public string? Genre { get; set; }

    [EnumDataType(typeof(TrackStatus))]
    public TrackStatus? Status { get; set; }
}
