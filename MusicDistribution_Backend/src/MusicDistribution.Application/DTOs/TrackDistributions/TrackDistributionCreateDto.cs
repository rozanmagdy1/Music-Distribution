using System.ComponentModel.DataAnnotations;

namespace MusicDistribution.Application.DTOs.TrackDistributions;

public class TrackDistributionCreateDto : IValidatableObject
{
    [Required]
    [MinLength(1)]
    public List<int> DspIds { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DspIds is null)
        {
            yield break;
        }

        if (DspIds.Any(id => id <= 0))
        {
            yield return new ValidationResult("DSP IDs must be positive.", [nameof(DspIds)]);
        }

        if (DspIds.Distinct().Count() != DspIds.Count)
        {
            yield return new ValidationResult("DSP IDs must not contain duplicates.", [nameof(DspIds)]);
        }
    }
}
