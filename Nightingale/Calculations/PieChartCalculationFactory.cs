using Nightingale.Figures;
using System;
using System.Linq;

namespace Nightingale.Calculations
{
    internal class PieChartCalculationFactory : MainCalculationFactory
    {
        public PieChartCalculationFactory(Chart chart) : base(chart)
        {
        }

        public float Radius => Math.Min(chart.CanvasSize.Width / 2.5f, chart.CanvasSize.Height / 2.5f);

        public float CenterX => chart.CanvasSize.Width / 2;

        public float CenterY => chart.CanvasSize.Height / 2;

        public float CalculateSweepAngle(SeriesValue seriesValue) => 360f * seriesValue.Value / chart.Series.Sum(x => x.Value);

        public float CalculateStartAngle()
            => chart.Shapes.Any() ?
                chart.Shapes.Sum(x => GetSweepAngle(x)) :
                0;

        private float GetSweepAngle(Shape shape)
        {
            if(shape is Slice)
            {
                return ((Slice)shape).SweepAngle;
            }
            else
            {
                return ((Arc)shape).SweepAngle;
            }
        }

        public float CalculateLabelXAxis(SeriesValue value)
            => chart.Series.IndexOf(value) % 2 == 0 ?
                    MarginX :
                    chart.CanvasSize.Width - 20;

        public float CalculateLabelYAxis(SeriesValue value)
        {
            var margin = AvaibleHeight / (20 / 2);

            var start = (chart.Series.IndexOf(value) / 2) + 1;

            return chart.TextSize * 2 * start;
        }

        public float CalculatePercentage(SeriesValue value) => value.Value * 100 / chart.Series.Sum(x => x.Value);
    }
}