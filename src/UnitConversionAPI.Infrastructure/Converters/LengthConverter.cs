using UnitConversionAPI.Core.Enums;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Infrastructure.Converters;

public class LengthConverter : IUnitConverter
{
    public string Category => "Length";

    private readonly Dictionary<string, double> _toMeters = new(StringComparer.OrdinalIgnoreCase)
    {
        { "meter", 1 },
        { "kilometer", 1000 },
        { "centimeter", 0.01 },
        { "millimeter", 0.001 },
        { "mile", 1609.34 },
        { "yard", 0.9144 },
        { "foot", 0.3048 },
        { "inch", 0.0254 }
    };

    public ConversionResult Convert(ConversionRequest request)
    {
        if (!_toMeters.ContainsKey(request.FromUnit) || !_toMeters.ContainsKey(request.ToUnit))
        {
            throw new ConversionException(
                $"Cannot convert from '{request.FromUnit}' to '{request.ToUnit}' in {Category} category.",
                ErrorCode.InvalidUnit);
        }

        var valueInMeters = request.Value * _toMeters[request.FromUnit];
        var convertedValue = valueInMeters / _toMeters[request.ToUnit];

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
        return _toMeters.ContainsKey(fromUnit) && _toMeters.ContainsKey(toUnit);
    }

    public IEnumerable<string> GetSupportedUnits()
    {
        return _toMeters.Keys;
    }
}
