using System;

namespace Nightingale.Calculations
{
    internal class VerticalBarChartCalculationFactory : BarChartCalculationFactory
    {
        public VerticalBarChartCalculationFactory(Chart chart) 
            : base(chart)
        {
        }

        public override float CalculateBottom() => AxisX;

        public override float CalculateLeft(float barStartingPoint) => barStartingPoint - (BarSize / 2);

        public override float CalculateRight(float barStartingPoint) => barStartingPoint + (BarSize / 2);

        public override float CalculateTop(SeriesValue value)
        {
            var percentage = value.Value / (chart.MaxEntryValue / AxisX) * 100 / AxisX;
            var increaseHeight = Math.Abs(percentage * ((chart.AllNegatives ? -AvaibleHeight : -MarginY) + AxisX) / 100);
            var result = AxisX - (value.Value > 0 ? increaseHeight : -increaseHeight);
            return result;
        }

        protected override void MeasureBarSizeAndDistance()
        {
            SpaceBetweenBars = (AvaibleWidth / chart.Series.Count) / 4;
            BarSize = (AvaibleWidth - (SpaceBetweenBars * (chart.Series.Count * 2))) / chart.Series.Count;
        }

        public override float CalculateRight(SeriesValue value) => throw new NotImplementedException();

        public override float CalculateTop(float barStartingPoint) => throw new NotImplementedException();

        public override float CalculateBottom(float barStartingPoint) => throw new NotImplementedException();
    }
}
