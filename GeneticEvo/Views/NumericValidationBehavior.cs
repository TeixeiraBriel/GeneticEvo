namespace GeneticEvo
{
    internal class NumericValidationBehavior
    {
        public Style InvalidStyle { get; set; }
        public Style ValidStyle { get; set; }
        public object Flags { get; set; }
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
        public int MaximumDecimalPlaces { get; set; }
    }
}