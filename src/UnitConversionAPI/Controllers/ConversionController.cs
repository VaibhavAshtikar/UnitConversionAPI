using Microsoft.AspNetCore.Mvc;
using UnitConversionAPI.Application.DTOs;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _conversionService;
    private readonly ILogger<ConversionController> _logger;

    public ConversionController(IConversionService conversionService, ILogger<ConversionController> logger)
    {
        _conversionService = conversionService;
        _logger = logger;
    }

    [HttpPost("convert")]
    public IActionResult Convert([FromBody] ConversionRequestDto requestDto)
    {
        try
        {
            var request = new ConversionRequest
            {
                Value = requestDto.Value,
                FromUnit = requestDto.FromUnit,
                ToUnit = requestDto.ToUnit,
                Category = requestDto.Category
            };

            var result = _conversionService.ConvertUnit(request);

            var response = new ConversionResponseDto
            {
                OriginalValue = result.OriginalValue,
                FromUnit = result.FromUnit,
                ConvertedValue = result.ConvertedValue,
                ToUnit = result.ToUnit,
                Category = result.Category,
                Formula = result.Formula
            };

            return Ok(response);
        }
        catch (ConversionException ex)
        {
            _logger.LogWarning(ex, "Conversion error occurred");
            return BadRequest(new ErrorResponseDto
            {
                Error = ex.Message,
                ErrorCode = ex.ErrorCode.ToString()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred");
            return StatusCode(500, new ErrorResponseDto
            {
                Error = "An unexpected error occurred",
                ErrorCode = "INTERNAL_ERROR"
            });
        }
    }
}
