using Microsoft.AspNetCore.Mvc;

namespace unitconverterApi.Controllers
{
    /// <summary>
    /// Main controller providing information about available conversion types
    /// </summary>
    [ApiController]
    [Route("convert")]
    public class ConversionController : ControllerBase
    {
        /// <summary>
        /// Get information about all available conversion types
        /// </summary>
        /// <returns>A list of available conversion types and usage instructions</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAvailableConversions()
        {
            var conversions = new[]
            {
                new
                {
                    Type = "Length",
                    Endpoint = "/convert/length",
                    UnitsEndpoint = "/convert/length/units"
                },
                new
                {
                    Type = "Weight",
                    Endpoint = "/convert/weight",
                    UnitsEndpoint = "/convert/weight/units"
                },
                new
                {
                    Type = "Temperature",
                    Endpoint = "/convert/temperature",
                    UnitsEndpoint = "/convert/temperature/units"
                }
            };

            return Ok(new
            {
                AvailableConversions = conversions,
                Usage = new
                {
                    GetUnits = "GET /convert/{type}/units",
                    Convert = "POST /convert/{type} with JSON body: { 'value': number, 'fromUnit': string, 'toUnit': string }"
                }
            });
        }
    }
}