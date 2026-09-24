using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.DTOs.Tracks;

public class TrackStatusUpdateDto
{
    [Required]
    [EnumDataType(typeof(TrackStatus))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TrackStatus? Status { get; set; }
}
