namespace UnitConversionAPI.Application.DTOs;

public class ConversionResponseDto
{
    public double OriginalValue { get; set; }
    public required string FromUnit { get; set; }
    public double ConvertedValue { get; set; }
    public required string ToUnit { get; set; }
    public required string Category { get; set; }
    public required string Formula { get; set; }
}
