# UnitConversionAPI

This repository contains an ASP.NET Core 9.0 Web API for unit conversions (Length, Temperature, Weight) implemented with a Clean Architecture split across Core, Application, Infrastructure and API layers. It also includes xUnit tests.

Quick start:

1. Restore packages:

```bash
dotnet restore
```

2. Build the solution:

```bash
dotnet build
```

3. Run the API:

```bash
cd src/UnitConversionAPI
dotnet run
```

4. Run tests:

```bash
cd ../../
dotnet test
```

API endpoint: POST `/api/conversion/convert` with JSON { value, fromUnit, toUnit, category }

Supported categories and units:
- Length: meter, kilometer, centimeter, millimeter, mile, yard, foot, inch
- Temperature: celsius, fahrenheit, kelvin
- Weight: kilogram, gram, milligram, pound, ounce, ton, stone

See the `src` and `tests` folders for full source and tests.

Addtional Note: Addtional work we can do this via Docker as well in aspect of pipeline built like build solution run unit test and check sanity testing 
