using Microsoft.AspNetCore.Mvc;
using unitconverterApi.Models;

namespace unitconverterApi.Controllers
{
    /// <summary>
    /// Controller for temperature unit conversions
    /// </summary>
    [ApiController]
    [Route("convert/[controller]")]
    public class TemperatureController : ControllerBase
    {
        /// <summary>
        /// Get all available temperature units
        /// </summary>
        /// <returns>A list of available temperature units</returns>
        [HttpGet("units")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvailableUnits()
        {
            var units = Enum.GetNames(typeof(TemperatureUnit))
                .Select(name => new
                {
                    Name = name,
                    Value = name
                });

            return Ok(new
            {
                UnitType = "Temperature",
                AvailableUnits = units
            });
        }

        /// <summary>
        /// Convert a value from one temperature unit to another
        /// </summary>
        /// <param name="request">The conversion request containing value, fromUnit, and toUnit</param>
        /// <returns>The conversion result</returns>
        /// <response code="200">Returns the conversion result</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Convert(ConversionRequest request)
        {
            try
            {
                if (!Enum.TryParse<TemperatureUnit>(request.FromUnit, true, out var fromUnit) ||
                    !Enum.TryParse<TemperatureUnit>(request.ToUnit, true, out var toUnit))
                {
                    return BadRequest(new ConversionResponse
                    {
                        Success = false,
                        ErrorMessage = "Invalid temperature unit specified"
                    });
                }

                double result = ConvertTemperature(request.Value, fromUnit, toUnit);

                return Ok(new ConversionResponse
                {
                    Value = request.Value,
                    FromUnit = request.FromUnit,
                    ToUnit = request.ToUnit,
                    Result = result,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ConversionResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Converts a temperature value from one unit to another
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="fromUnit">The source temperature unit</param>
        /// <param name="toUnit">The target temperature unit</param>
        /// <returns>The converted temperature value</returns>
        /// <exception cref="ArgumentException">Thrown when an unsupported temperature unit is specified</exception>
        private static double ConvertTemperature(double value, TemperatureUnit fromUnit, TemperatureUnit toUnit)
        {
            // Convert to Celsius first (base unit)
            double celsius = fromUnit switch
            {
                TemperatureUnit.Celsius => value,
                TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
                TemperatureUnit.Kelvin => value - 273.15,
                _ => throw new ArgumentException("Unsupported temperature unit")
            };

            // Convert from Celsius to target unit
            return toUnit switch
            {
                TemperatureUnit.Celsius => celsius,
                TemperatureUnit.Fahrenheit => (celsius * 9 / 5) + 32,
                TemperatureUnit.Kelvin => celsius + 273.15,
                _ => throw new ArgumentException("Unsupported temperature unit")
            };
        }
    }
}