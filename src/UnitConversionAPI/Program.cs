using UnitConversionAPI.Application.Services;
using UnitConversionAPI.Core.Interfaces;
using UnitConversionAPI.Infrastructure.Converters;
using UnitConversionAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Unit Conversion API",
        Version = "v1",
        Description = @"API for converting values between different units of measurement.

**Available Categories:**
- **Length**: meter, kilometer, centimeter, millimeter, mile, yard, foot, inch
- **Temperature**: celsius, fahrenheit, kelvin
- **Weight**: kilogram, gram, milligram, pound, ounce, ton, stone"
    });
});

builder.Services.AddSingleton<IUnitConverter, LengthConverter>();
builder.Services.AddSingleton<IUnitConverter, TemperatureConverter>();
builder.Services.AddSingleton<IUnitConverter, WeightConverter>();
builder.Services.AddSingleton<IConversionService, ConversionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
