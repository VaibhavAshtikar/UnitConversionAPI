using UnitConversionAPI.Core.Models;

namespace UnitConversionAPI.Core.Interfaces;

public interface IUnitConverter
{
    string Category { get; }
    ConversionResult Convert(ConversionRequest request);
    bool CanConvert(string fromUnit, string toUnit);
    IEnumerable<string> GetSupportedUnits();
}
