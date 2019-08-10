using Nightingale.Figures;
using SkiaSharp;
using System.Linq;

namespace Nightingale.Charts
{
    public class PieChart : CircularChart
    {
        public override Shape Create(SeriesValue seriesValue)
        {
            float sweepAngle = 360f * seriesValue.Value / Series.Sum(x => x.Value);
            var paint = new SKPaint
            {
                StrokeWidth = 30,
                Style = SKPaintStyle.Fill,
                Color = seriesValue.HasColour() ? seriesValue.Colour : GetDefaultColour(seriesValue),
            };

            var labelPosition = new SKPoint(GetXAxisFor(seriesValue), GetYAxisFor(seriesValue));
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

            if(Series.IndexOf(seriesValue) > 0)
            {
                var previous = shapes.Last() as Slice;
                StartAngle += previous.SweepAngle;
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
                StartAngle = StartAngle,
                SweepAngle = sweepAngle,
                Text = text,
                TextPaint = textPaint
            };
        }

        protected override void DrawChart()
        {
            StartAngle = 0;
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}
