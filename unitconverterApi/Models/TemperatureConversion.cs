namespace unitconverterApi.Models
{
    /// <summary>
    /// Represents a temperature conversion operation
    /// </summary>
    public class TemperatureConversion
    {
        /// <summary>
        /// The value to convert
        /// </summary>
        public double Value { get; set; }
        
        /// <summary>
        /// The unit to convert from
        /// </summary>
        public TemperatureUnit FromUnit { get; set; }
        
        /// <summary>
        /// The unit to convert to
        /// </summary>
        public TemperatureUnit ToUnit { get; set; }
        
        /// <summary>
        /// The conversion result
        /// </summary>
        public double Result { get; set; }
    }

    /// <summary>
    /// Available temperature units for conversion
    /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>
        /// Celsius (°C)
        /// </summary>
        Celsius,
        
        /// <summary>
        /// Fahrenheit (°F)
        /// </summary>
        Fahrenheit,
        
        /// <summary>
        /// Kelvin (K)
        /// </summary>
        Kelvin
    }
}
