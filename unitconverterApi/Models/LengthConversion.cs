namespace unitconverterApi.Models
{
    /// <summary>
    /// Represents a length conversion operation
    /// </summary>
    public class LengthConversion
    {
        /// <summary>
        /// The value to convert
        /// </summary>
        public double Value { get; set; }
        
        /// <summary>
        /// The unit to convert from
        /// </summary>
        public LengthUnit FromUnit { get; set; }
        
        /// <summary>
        /// The unit to convert to
        /// </summary>
        public LengthUnit ToUnit { get; set; }
        
        /// <summary>
        /// The conversion result
        /// </summary>
        public double Result { get; set; }
    }

    /// <summary>
    /// Available length units for conversion
    /// </summary>
    public enum LengthUnit
    {
        /// <summary>
        /// Millimeter (mm)
        /// </summary>
        Millimeter,
        
        /// <summary>
        /// Centimeter (cm)
        /// </summary>
        Centimeter,
        
        /// <summary>
        /// Meter (m)
        /// </summary>
        Meter,
        
        /// <summary>
        /// Kilometer (km)
        /// </summary>
        Kilometer,
        
        /// <summary>
        /// Inch (in)
        /// </summary>
        Inch,
        
        /// <summary>
        /// Foot (ft)
        /// </summary>
        Foot,
        
        /// <summary>
        /// Yard (yd)
        /// </summary>
        Yard,
        
        /// <summary>
        /// Mile (mi)
        /// </summary>
        Mile
    }
}
