using Microsoft.AspNetCore.Mvc;
using unitconverterApi.Models;

namespace unitconverterApi.Controllers
{
    /// <summary>
    /// Controller for length unit conversions
    /// </summary>
    [ApiController]
    [Route("convert/[controller]")]
    public class LengthController : ControllerBase
    {
        /// <summary>
        /// Get all available length units
        /// </summary>
        /// <returns>A list of available length units</returns>
        [HttpGet("units")]
        public IActionResult GetAvailableUnits()
        {
            var units = Enum.GetNames(typeof(LengthUnit))
                .Select(name => new
                {
                    Name = name,
                    Value = name
                });

            return Ok(new
            {
                UnitType = "Length",
                AvailableUnits = units
            });
        }

        /// <summary>
        /// Convert a value from one length unit to another
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
                if (!Enum.TryParse<LengthUnit>(request.FromUnit, true, out var fromUnit) ||
                    !Enum.TryParse<LengthUnit>(request.ToUnit, true, out var toUnit))
                {
                    return BadRequest(new ConversionResponse
                    {
                        Success = false,
                        ErrorMessage = "Invalid length unit specified"
                    });
                }

                double result = ConvertLength(request.Value, fromUnit, toUnit);

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

        private static double ConvertLength(double value, LengthUnit fromUnit, LengthUnit toUnit)
        {
            // Convert to meters first (base unit)
            double meters = fromUnit switch
            {
                LengthUnit.Millimeter => value / 1000,
                LengthUnit.Centimeter => value / 100,
                LengthUnit.Meter => value,
                LengthUnit.Kilometer => value * 1000,
                LengthUnit.Inch => value * 0.0254,
                LengthUnit.Foot => value * 0.3048,
                LengthUnit.Yard => value * 0.9144,
                LengthUnit.Mile => value * 1609.344,
                _ => throw new ArgumentException("Unsupported length unit")
            };

            // Convert from meters to target unit
            return toUnit switch
            {
                LengthUnit.Millimeter => meters * 1000,
                LengthUnit.Centimeter => meters * 100,
                LengthUnit.Meter => meters,
                LengthUnit.Kilometer => meters / 1000,
                LengthUnit.Inch => meters / 0.0254,
                LengthUnit.Foot => meters / 0.3048,
                LengthUnit.Yard => meters / 0.9144,
                LengthUnit.Mile => meters / 1609.344,
                _ => throw new ArgumentException("Unsupported length unit")
            };
        }
    }
}