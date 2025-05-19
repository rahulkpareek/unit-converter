namespace unitconverterApi.Models
{
    /// <summary>
    /// Represents a weight conversion operation
    /// </summary>
    public class WeightConversion
    {
        /// <summary>
        /// The value to convert
        /// </summary>
        public double Value { get; set; }
        
        /// <summary>
        /// The unit to convert from
        /// </summary>
        public WeightUnit FromUnit { get; set; }
        
        /// <summary>
        /// The unit to convert to
        /// </summary>
        public WeightUnit ToUnit { get; set; }
        
        /// <summary>
        /// The conversion result
        /// </summary>
        public double Result { get; set; }
    }

    /// <summary>
    /// Available weight units for conversion
    /// </summary>
    public enum WeightUnit
    {
        /// <summary>
        /// Milligram (mg)
        /// </summary>
        Milligram,
        
        /// <summary>
        /// Gram (g)
        /// </summary>
        Gram,
        
        /// <summary>
        /// Kilogram (kg)
        /// </summary>
        Kilogram,
        
        /// <summary>
        /// Metric Ton (t)
        /// </summary>
        MetricTon,
        
        /// <summary>
        /// Ounce (oz)
        /// </summary>
        Ounce,
        
        /// <summary>
        /// Pound (lb)
        /// </summary>
        Pound,
        
        /// <summary>
        /// Stone (st)
        /// </summary>
        Stone,
        
        /// <summary>
        /// US Ton (short ton)
        /// </summary>
        USTon
    }
}
