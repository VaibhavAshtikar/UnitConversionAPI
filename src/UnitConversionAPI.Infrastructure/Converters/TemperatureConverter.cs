using UnitConversionAPI.Core.Enums;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Infrastructure.Converters;

public class TemperatureConverter : IUnitConverter
{
    public string Category => "Temperature";

    private readonly HashSet<string> _supportedUnits = new(StringComparer.OrdinalIgnoreCase)
    {
        "celsius", "fahrenheit", "kelvin"
    };

    public ConversionResult Convert(ConversionRequest request)
    {
        if (!_supportedUnits.Contains(request.FromUnit) || !_supportedUnits.Contains(request.ToUnit))
        {
            throw new ConversionException(
                $"Cannot convert from '{request.FromUnit}' to '{request.ToUnit}' in {Category} category.",
                ErrorCode.InvalidUnit);
        }

        var celsius = ToCelsius(request.Value, request.FromUnit);
        var convertedValue = FromCelsius(celsius, request.ToUnit);

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
        return _supportedUnits.Contains(fromUnit) && _supportedUnits.Contains(toUnit);
    }

    public IEnumerable<string> GetSupportedUnits()
    {
        return _supportedUnits;
    }

    private double ToCelsius(double value, string fromUnit)
    {
        return fromUnit.ToLower() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32) * 5 / 9,
            "kelvin" => value - 273.15,
            _ => throw new ConversionException($"Unknown unit: {fromUnit}", ErrorCode.InvalidUnit)
        };
    }

    private double FromCelsius(double celsius, string toUnit)
    {
        return toUnit.ToLower() switch
        {
            "celsius" => celsius,
            "fahrenheit" => celsius * 9 / 5 + 32,
            "kelvin" => celsius + 273.15,
            _ => throw new ConversionException($"Unknown unit: {toUnit}", ErrorCode.InvalidUnit)
        };
    }
}
