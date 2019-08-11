using Nightingale.Calculations;
using Nightingale.Figures;
using SkiaSharp;

namespace Nightingale.Charts
{
    public class BarChart : Chart
    {
        internal override MainCalculationFactory CreateFactory() => new BarChartCalculationFactory(this);

        public override Shape Create(SeriesValue value)
        {
            var factory = calculationFactory as BarChartCalculationFactory;

            var paint = new SKPaint
            {
                Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                StrokeWidth = factory.BarSize,
                TextSize = TextSize,
                TextAlign = SKTextAlign.Center
            };

            if (!value.Focused && shapeTouched)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                paint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, sigma);
            }

            var barStartingPoint = factory.CalculateBarStartingPoint(Series.IndexOf(value));

            var left = factory.CalculateLeft(barStartingPoint);
            var right = factory.CalculateRight(barStartingPoint);
            var bottom = calculationFactory.AxisX;
            var top = factory.CalculateTop(value);

            var shape = new Bar(canvas)
            {
                Paint = paint,
                Rect = new SKRect(left, top, right, bottom),
                LabelPosition = new SKPoint(barStartingPoint, CanvasSize.Height - 40),
                ValuePosition = new SKPoint(barStartingPoint, CanvasSize.Height - 10),
                SerieValue = value
            };

            return shape;
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