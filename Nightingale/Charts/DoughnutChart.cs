using Nightingale.Calculations;
using Nightingale.Figures;
using SkiaSharp;

namespace Nightingale.Charts
{
    public class DoughnutChart : CircularChart
    {
        public override Shape Create(SeriesValue seriesValue)
        {
            var factory = calculationFactory as PieChartCalculationFactory;

            var paint = new SKPaint
            {
                StrokeWidth = 40,
                Style = SKPaintStyle.Stroke,
                Color = seriesValue.HasColour() ? seriesValue.Colour : GetDefaultColour(seriesValue),
            };

            var labelPosition = new SKPoint(factory.CalculateLabelXAxis(seriesValue), factory.CalculateLabelYAxis(seriesValue));
            var leftSide = Series.IndexOf(seriesValue) % 2 == 0;

            var text = leftSide ?
                        $"{seriesValue.Label} {(UseCaption() ? seriesValue.Caption : CalculatePercentage(seriesValue).ToString("F0") + "%")}" :
                        $"{(UseCaption() ? seriesValue.Caption : CalculatePercentage(seriesValue).ToString("F0") + "%")} {seriesValue.Label}";

            if (!leftSide)
            {
                var width = paint.MeasureText(text);
                labelPosition = new SKPoint(labelPosition.X - 20 - width, labelPosition.Y);
            }

            var textPaint = new SKPaint { Color = paint.Color };

            if (!seriesValue.Focused && shapeTouched)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                paint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, sigma);
                textPaint.MaskFilter = paint.MaskFilter;
            }

            return new Arc(canvas)
            {
                SerieValue = seriesValue,
                CenterRect = CenterRect,
                Paint = paint,
                LabelPosition = labelPosition,
                StartAngle = factory.CalculateStartAngle(),
                SweepAngle = factory.CalculateSweepAngle(seriesValue),
                Text = text,
                TextPaint = textPaint
            };
        }

        protected override void DrawChart()
        {
            foreach (var shape in Shapes)
            {
                shape.Draw();
            }
        }
    }
}
