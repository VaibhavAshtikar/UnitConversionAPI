using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Core.Interfaces;

public interface IConversionService
{
    ConversionResult ConvertUnit(ConversionRequest request);
    IReadOnlyList<string> GetSupportedCategories();
    IReadOnlyList<string> GetUnitsForCategory(string category);
}
