namespace unitconverterApi.Models
{
    /// <summary>
    /// Represents a unit conversion request
    /// </summary>
    public class ConversionRequest
    {
        /// <summary>
        /// The value to convert
        /// </summary>
        public double Value { get; set; }
        
        /// <summary>
        /// The unit to convert from
        /// </summary>
        public string FromUnit { get; set; } = string.Empty;
        
        /// <summary>
        /// The unit to convert to
        /// </summary>
        public string ToUnit { get; set; } = string.Empty;
    }
}