using System;

namespace Nightingale.Calculations
{
    internal class HorizontalBarChartCalculationFactory : BarChartCalculationFactory
    {
        public HorizontalBarChartCalculationFactory(Chart chart) 
            : base(chart)
        {

        }

        public override float CalculateLeft(float barStartingPoint) => AxisY;

        public override float CalculateRight(SeriesValue value)
        {
            var percentage = Math.Abs(value.Value / (chart.MaxEntryValue / AvaibleWidth) * 100 / AvaibleWidth);

            if (!chart.HasNegativeValues)
            {
                var increase = Math.Abs(percentage * AvaibleWidth / 100);
                increase += AxisY;
                return increase;
            }
            else
            {
                var middlePoint = (AvaibleWidth / 2);
                var increase = Math.Abs((percentage * middlePoint) / 100);
                var direction = value.Value > 0 ? increase : -increase;
                var result = AxisY + direction;
                return result;
            }            
        }

        public override float CalculateBottom(float barStartingPoint) => barStartingPoint - (BarSize / 2);

        public override float CalculateTop(float barStartingPoint) => barStartingPoint + (BarSize / 2);

        protected override void MeasureBarSizeAndDistance()
        {
            SpaceBetweenBars = (AvaibleHeight / chart.Series.Count) / 4;
            BarSize = (AvaibleHeight - (SpaceBetweenBars * (chart.Series.Count * 2))) / chart.Series.Count;
        }

        protected override void MeasureMargins()
        {
            base.MeasureMargins();
            MarginY = chart.CanvasSize.Height * 10 / 100;
            MarginX = chart.CanvasSize.Width * 15 / 100;
        }

        protected override void MeasureAxis()
        {
            base.MeasureAxis();
            AxisY = chart.HasNegativeValues ? chart.CanvasSize.Width / 2 : MarginX / 2;
        }

        public override float GetLabelPointY() => MarginY;

        public override float GetLabelValuePointY() => chart.CanvasSize.Width - (MarginY / 2);

        public override float CalculateRight(float barStartingPoint) => throw new NotImplementedException();

        public override float CalculateTop(SeriesValue value) => throw new NotImplementedException();

        public override float CalculateBottom() => throw new NotImplementedException();
    }
}
