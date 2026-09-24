using System.Text.Json.Serialization;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.DTOs.TrackDistributions;

public class TrackDistributionDto
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public int DspId { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DistributionStatus Status { get; set; }
}
