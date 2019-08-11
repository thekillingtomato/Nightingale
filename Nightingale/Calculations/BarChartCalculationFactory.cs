using System;

namespace Nightingale.Calculations
{
    internal class BarChartCalculationFactory : MainCalculationFactory
    {
        public BarChartCalculationFactory(Chart chart) : base(chart)
        {
            MeasureBarSizeAndDistance();
        }

        protected override void MeasureMargins()
        {
            base.MeasureMargins();
            MarginY = chart.CanvasSize.Height * (chart.AllNegatives || chart.AllPositive ? 25 : 15) / 100;
        }

        private void MeasureBarSizeAndDistance()
        {
            SpaceBetweenBars = (AvaibleWidth / chart.Series.Count) / 4;
            BarSize = (AvaibleWidth - (SpaceBetweenBars * (chart.Series.Count * 2))) / chart.Series.Count;
        }

        public float SpaceBetweenBars { get; private set; }

        public float BarSize { get; private set; }

        public float CalculateBarStartingPoint(int index)
        {
            return (index * BarSize + SpaceBetweenBars) * 2;
        }

        public float CalculateLeft(float barStartingPoint)
        {
            return barStartingPoint - (BarSize / 2);
        }

        public float CalculateRight(float barStartingPoint)
        {
            return barStartingPoint + (BarSize / 2);
        }

        public float CalculateTop(SeriesValue value)
        {
            var percentage = value.Value / (chart.MaxEntryValue / AxisX) * 100 / AxisX;
            var increaseHeight = Math.Abs(percentage * ((chart.AllNegatives ? -AvaibleHeight : -MarginY) + AxisX) / 100);
            var result = AxisX - (value.Value > 0 ? increaseHeight : -increaseHeight);
            return result;
        }
    }
}
