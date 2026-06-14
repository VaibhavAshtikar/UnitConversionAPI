using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitConversionAPI.Application.DTOs;
using UnitConversionAPI.Controllers;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;
using Xunit;

namespace UnitConversionAPI.Tests;

public class ConversionControllerTests
{
    private readonly Mock<IConversionService> _mockService;
    private readonly ConversionController _controller;

    public ConversionControllerTests()
    {
        _mockService = new Mock<IConversionService>();
        var mockLogger = new Mock<ILogger<ConversionController>>();
        _controller = new ConversionController(_mockService.Object, mockLogger.Object);
    }

    [Fact]
    public void Convert_ValidRequest_ReturnsOkResult()
    {
        var request = new ConversionRequestDto
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "foot",
            Category = "Length"
        };

        _mockService.Setup(x => x.ConvertUnit(It.IsAny<ConversionRequest>()))
            .Returns(new ConversionResult
            {
                OriginalValue = 10,
                FromUnit = "meter",
                ConvertedValue = 32.8,
                ToUnit = "foot",
                Category = "Length",
                Formula = "10 meter = 32.8 foot"
            });

        var result = _controller.Convert(request);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Convert_ConversionException_ReturnsBadRequest()
    {
        var request = new ConversionRequestDto
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "invalid",
            Category = "Length"
        };

        _mockService.Setup(x => x.ConvertUnit(It.IsAny<ConversionRequest>()))
            .Throws(new ConversionException("Cannot convert"));

        var result = _controller.Convert(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Convert_GenericException_Returns500()
    {
        var request = new ConversionRequestDto
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "foot",
            Category = "Length"
        };

        _mockService.Setup(x => x.ConvertUnit(It.IsAny<ConversionRequest>()))
            .Throws(new Exception("Unexpected error"));

        var result = _controller.Convert(request);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
    }
}
