namespace unitconverterApi.Models
{
    /// <summary>
    /// Represents a unit conversion response
    /// </summary>
    public class ConversionResponse
    {
        /// <summary>
        /// The original value to convert
        /// </summary>
        public double Value { get; set; }
        
        /// <summary>
        /// The unit converted from
        /// </summary>
        public string FromUnit { get; set; } = string.Empty;
        
        /// <summary>
        /// The unit converted to
        /// </summary>
        public string ToUnit { get; set; } = string.Empty;
        
        /// <summary>
        /// The conversion result
        /// </summary>
        public double Result { get; set; }
        
        /// <summary>
        /// Indicates whether the conversion was successful
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Error message if the conversion failed
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}