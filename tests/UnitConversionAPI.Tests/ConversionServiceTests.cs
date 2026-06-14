using Moq;
using UnitConversionAPI.Application.Services;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;
using Xunit;

namespace UnitConversionAPI.Tests;

public class ConversionServiceTests
{
    [Fact]
    public void ConvertUnit_ValidCategory_CallsCorrectConverter()
    {
        var mockConverter = new Mock<IUnitConverter>();
        mockConverter.Setup(c => c.Category).Returns("Length");
        mockConverter.Setup(c => c.Convert(It.IsAny<ConversionRequest>()))
            .Returns(new ConversionResult
            {
                OriginalValue = 10,
                FromUnit = "meter",
                ConvertedValue = 32.8,
                ToUnit = "foot",
                Category = "Length",
                Formula = "10 meter = 32.8 foot"
            });

        var service = new ConversionService(new[] { mockConverter.Object });
        var request = new ConversionRequest { Value = 10, FromUnit = "meter", ToUnit = "foot", Category = "Length" };

        service.ConvertUnit(request);

        mockConverter.Verify(c => c.Convert(It.IsAny<ConversionRequest>()), Times.Once);
    }

    [Fact]
    public void ConvertUnit_InvalidCategory_ThrowsException()
    {
        var service = new ConversionService(Array.Empty<IUnitConverter>());
        var request = new ConversionRequest { Value = 10, FromUnit = "meter", ToUnit = "foot", Category = "Invalid" };

        Assert.Throws<ConversionException>(() => service.ConvertUnit(request));
    }

    [Fact]
    public void GetSupportedCategories_ReturnsAllCategories()
    {
        var converters = new[]
        {
            Mock.Of<IUnitConverter>(c => c.Category == "Length"),
            Mock.Of<IUnitConverter>(c => c.Category == "Temperature"),
            Mock.Of<IUnitConverter>(c => c.Category == "Weight")
        };

        var service = new ConversionService(converters);
        var categories = service.GetSupportedCategories();

        Assert.Equal(3, categories.Count);
    }
}
