using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Models;
using UnitConversionAPI.Infrastructure.Converters;
using Xunit;

namespace UnitConversionAPI.Tests;

public class WeightConverterTests
{
    private readonly WeightConverter _converter = new();

    [Theory]
    [InlineData(1, "kilogram", "pound", 2.20462)]
    [InlineData(1000, "gram", "kilogram", 1)]
    [InlineData(1, "pound", "ounce", 16)]
    [InlineData(1, "ton", "kilogram", 1000)]
    public void Convert_ValidUnits_ReturnsCorrectValue(double value, string from, string to, double expected)
    {
        var request = new ConversionRequest { Value = value, FromUnit = from, ToUnit = to, Category = "Weight" };
        var result = _converter.Convert(request);
        Assert.Equal(expected, result.ConvertedValue, precision: 4);
    }

    [Theory]
    [InlineData("invalid", "kilogram")]
    [InlineData("kilogram", "invalid")]
    public void Convert_InvalidUnit_ThrowsException(string from, string to)
    {
        var request = new ConversionRequest { Value = 10, FromUnit = from, ToUnit = to, Category = "Weight" };
        Assert.Throws<ConversionException>(() => _converter.Convert(request));
    }

    [Fact]
    public void GetSupportedUnits_Returns7Units()
    {
        Assert.Equal(7, _converter.GetSupportedUnits().Count());
    }
}
