using UnitConversionAPI.Core.Enums;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Infrastructure.Converters;

public class WeightConverter : IUnitConverter
{
    public string Category => "Weight";

    private readonly Dictionary<string, double> _toKilograms = new(StringComparer.OrdinalIgnoreCase)
    {
        { "kilogram", 1 },
        { "gram", 0.001 },
        { "milligram", 0.000001 },
        { "pound", 0.453592 },
        { "ounce", 0.0283495 },
        { "ton", 1000 },
        { "stone", 6.35029 }
    };

    public ConversionResult Convert(ConversionRequest request)
    {
        if (!_toKilograms.ContainsKey(request.FromUnit) || !_toKilograms.ContainsKey(request.ToUnit))
        {
            throw new ConversionException(
                $"Cannot convert from '{request.FromUnit}' to '{request.ToUnit}' in {Category} category.",
                ErrorCode.InvalidUnit);
        }

        var valueInKilograms = request.Value * _toKilograms[request.FromUnit];
        var convertedValue = valueInKilograms / _toKilograms[request.ToUnit];

        return new ConversionResult
        {
            OriginalValue = request.Value,
            FromUnit = request.FromUnit,
            ConvertedValue = convertedValue,
            ToUnit = request.ToUnit,
            Category = Category,
            Formula = $"{request.Value} {request.FromUnit} = {convertedValue} {request.ToUnit}"
        };
    }

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return _toKilograms.ContainsKey(fromUnit) && _toKilograms.ContainsKey(toUnit);
    }

    public IEnumerable<string> GetSupportedUnits()
    {
        return _toKilograms.Keys;
    }
}
