using System.ComponentModel.DataAnnotations;

namespace UnitConversionAPI.Application.DTOs;

public class ConversionRequestDto
{
    [Required]
    public double Value { get; set; }

    [Required]
    public required string FromUnit { get; set; }

    [Required]
    public required string ToUnit { get; set; }

    [Required]
    public required string Category { get; set; }
}
