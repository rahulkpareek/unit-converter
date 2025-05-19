using Microsoft.AspNetCore.Mvc;
using unitconverterApi.Models;

namespace unitconverterApi.Controllers
{
    /// <summary>
    /// Controller for weight unit conversions
    /// </summary>
    [ApiController]
    [Route("convert/[controller]")]
    public class WeightController : ControllerBase
    {
        /// <summary>
        /// Get all available weight units
        /// </summary>
        /// <returns>A list of available weight units</returns>
        [HttpGet("units")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvailableUnits()
        {
            var units = Enum.GetNames(typeof(WeightUnit))
                .Select(name => new
                {
                    Name = name,
                    Value = name
                });

            return Ok(new
            {
                UnitType = "Weight",
                AvailableUnits = units
            });
        }

        /// <summary>
        /// Convert a value from one weight unit to another
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
                if (!Enum.TryParse<WeightUnit>(request.FromUnit, true, out var fromUnit) ||
                    !Enum.TryParse<WeightUnit>(request.ToUnit, true, out var toUnit))
                {
                    return BadRequest(new ConversionResponse
                    {
                        Success = false,
                        ErrorMessage = "Invalid weight unit specified"
                    });
                }

                double result = ConvertWeight(request.Value, fromUnit, toUnit);

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
        /// Converts a weight value from one unit to another
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="fromUnit">The source weight unit</param>
        /// <param name="toUnit">The target weight unit</param>
        /// <returns>The converted weight value</returns>
        /// <exception cref="ArgumentException">Thrown when an unsupported weight unit is specified</exception>
        private static double ConvertWeight(double value, WeightUnit fromUnit, WeightUnit toUnit)
        {
            // Convert to grams first (base unit)
            double grams = fromUnit switch
            {
                WeightUnit.Milligram => value / 1000,
                WeightUnit.Gram => value,
                WeightUnit.Kilogram => value * 1000,
                WeightUnit.MetricTon => value * 1000000,
                WeightUnit.Ounce => value * 28.3495,
                WeightUnit.Pound => value * 453.592,
                WeightUnit.Stone => value * 6350.29,
                WeightUnit.USTon => value * 907185,
                _ => throw new ArgumentException("Unsupported weight unit")
            };

            // Convert from grams to target unit
            return toUnit switch
            {
                WeightUnit.Milligram => grams * 1000,
                WeightUnit.Gram => grams,
                WeightUnit.Kilogram => grams / 1000,
                WeightUnit.MetricTon => grams / 1000000,
                WeightUnit.Ounce => grams / 28.3495,
                WeightUnit.Pound => grams / 453.592,
                WeightUnit.Stone => grams / 6350.29,
                WeightUnit.USTon => grams / 907185,
                _ => throw new ArgumentException("Unsupported weight unit")
            };
        }
    }
}