using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Models;
using UnitConversionAPI.Infrastructure.Converters;
using Xunit;

namespace UnitConversionAPI.Tests;

public class TemperatureConverterTests
{
    private readonly TemperatureConverter _converter = new();

    [Theory]
    [InlineData(0, "celsius", "fahrenheit", 32)]
    [InlineData(100, "celsius", "fahrenheit", 212)]
    [InlineData(32, "fahrenheit", "celsius", 0)]
    [InlineData(0, "celsius", "kelvin", 273.15)]
    public void Convert_ValidUnits_ReturnsCorrectValue(double value, string from, string to, double expected)
    {
        var request = new ConversionRequest { Value = value, FromUnit = from, ToUnit = to, Category = "Temperature" };
        var result = _converter.Convert(request);
        Assert.Equal(expected, result.ConvertedValue, precision: 2);
    }

    [Theory]
    [InlineData("invalid", "celsius")]
    [InlineData("celsius", "invalid")]
    public void Convert_InvalidUnit_ThrowsException(string from, string to)
    {
        var request = new ConversionRequest { Value = 10, FromUnit = from, ToUnit = to, Category = "Temperature" };
        Assert.Throws<ConversionException>(() => _converter.Convert(request));
    }

    [Fact]
    public void GetSupportedUnits_Returns3Units()
    {
        Assert.Equal(3, _converter.GetSupportedUnits().Count());
    }
}
