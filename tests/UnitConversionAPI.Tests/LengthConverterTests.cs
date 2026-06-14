using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Models;
using UnitConversionAPI.Infrastructure.Converters;
using Xunit;

namespace UnitConversionAPI.Tests;

public class LengthConverterTests
{
    private readonly LengthConverter _converter = new();

    [Theory]
    [InlineData(1, "meter", "meter", 1)]
    [InlineData(1, "meter", "kilometer", 0.001)]
    [InlineData(1, "meter", "foot", 3.28084)]
    [InlineData(1, "kilometer", "meter", 1000)]
    [InlineData(100, "centimeter", "meter", 1)]
    public void Convert_ValidUnits_ReturnsCorrectValue(double value, string from, string to, double expected)
    {
        var request = new ConversionRequest { Value = value, FromUnit = from, ToUnit = to, Category = "Length" };
        var result = _converter.Convert(request);
        Assert.Equal(expected, result.ConvertedValue, precision: 4);
    }

    [Theory]
    [InlineData("invalid", "meter")]
    [InlineData("meter", "invalid")]
    public void Convert_InvalidUnit_ThrowsException(string from, string to)
    {
        var request = new ConversionRequest { Value = 10, FromUnit = from, ToUnit = to, Category = "Length" };
        Assert.Throws<ConversionException>(() => _converter.Convert(request));
    }

    [Fact]
    public void GetSupportedUnits_Returns8Units()
    {
        Assert.Equal(8, _converter.GetSupportedUnits().Count());
    }
}
