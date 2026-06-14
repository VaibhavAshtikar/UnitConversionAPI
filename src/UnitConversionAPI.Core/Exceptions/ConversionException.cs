using UnitConversionAPI.Core.Enums;

namespace UnitConversionAPI.Core.Exceptions;

public class ConversionException : Exception
{
    public ErrorCode ErrorCode { get; }

    public ConversionException(string message, ErrorCode errorCode = ErrorCode.InternalError)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public ConversionException(string message, Exception innerException, ErrorCode errorCode = ErrorCode.InternalError)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
