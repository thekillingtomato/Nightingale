using Nightingale.Calculations;
using Nightingale.Figures;
using SkiaSharp;

namespace Nightingale.Charts
{
    public class PieChart : CircularChart
    {
        public override Shape Create(SeriesValue seriesValue)
        {
            var factory = calculationFactory as PieChartCalculationFactory;

            var paint = new SKPaint
            {
                StrokeWidth = 30,
                Style = SKPaintStyle.Fill,
                Color = seriesValue.HasColour() ? seriesValue.Colour : GetDefaultColour(seriesValue),
            };

            var textPaint = new SKPaint
            {
                Color = paint.Color,
                TextSize = TextSize
            };

            var labelPosition = new SKPoint(factory.CalculateLabelXAxis(seriesValue), factory.CalculateLabelYAxis(seriesValue));
            var leftSide = Series.IndexOf(seriesValue) % 2 == 0;

            var text = leftSide ?
                        $"{seriesValue.Label} {(UseCaption() ? seriesValue.Caption : CalculatePercentage(seriesValue).ToString("F0") + "%")}" :
                        $"{(UseCaption() ? seriesValue.Caption : CalculatePercentage(seriesValue).ToString("F0") + "%")} {seriesValue.Label}";

            if (!leftSide)
            {
                var width = textPaint.MeasureText(text);
                labelPosition = new SKPoint(labelPosition.X - width, labelPosition.Y);
            }

            if (!seriesValue.Focused && shapeTouched)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                paint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, sigma);
                textPaint.MaskFilter = paint.MaskFilter;
            }

            return new Slice(canvas)
            {
                SerieValue = seriesValue,
                CenterRect = CenterRect,
                Center = CenterPoint,
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