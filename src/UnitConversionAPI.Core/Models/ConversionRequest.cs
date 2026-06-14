namespace UnitConversionAPI.Core.Models;

public class ConversionRequest
{
    public double Value { get; set; }
    public required string FromUnit { get; set; }
    public required string ToUnit { get; set; }
    public required string Category { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(FromUnit) &&
               !string.IsNullOrWhiteSpace(ToUnit) &&
               !string.IsNullOrWhiteSpace(Category);
    }
}
