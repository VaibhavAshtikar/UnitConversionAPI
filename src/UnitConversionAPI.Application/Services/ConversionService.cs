using UnitConversionAPI.Core.Enums;
using UnitConversionAPI.Core.Exceptions;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Application.Services;

public class ConversionService : IConversionService
{
    private readonly Dictionary<string, IUnitConverter> _converterMap;

    public ConversionService(IEnumerable<IUnitConverter> converters)
    {
        _converterMap = converters.ToDictionary(c => c.Category, StringComparer.OrdinalIgnoreCase);
    }

    public ConversionResult ConvertUnit(ConversionRequest request)
    {
        if (!request.IsValid())
        {
            throw new ConversionException("Invalid conversion request", ErrorCode.InvalidValue);
        }

        if (!_converterMap.TryGetValue(request.Category, out var converter))
        {
            throw new ConversionException($"Category '{request.Category}' is not supported", ErrorCode.InvalidCategory);
        }

        return converter.Convert(request);
    }

    public IReadOnlyList<string> GetSupportedCategories()
    {
        return _converterMap.Keys.ToList();
    }

    public IReadOnlyList<string> GetUnitsForCategory(string category)
    {
        if (!_converterMap.TryGetValue(category, out var converter))
        {
            throw new ConversionException($"Category '{category}' is not supported", ErrorCode.InvalidCategory);
        }

        return converter.GetSupportedUnits().ToList();
    }
}
