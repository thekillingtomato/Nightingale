using Nightingale.Calculations;
using SkiaSharp;

namespace Nightingale.Drawable
{
    internal class BarDrawableFactory : MainDrawableFactory
    {
        internal BarDrawableFactory(BarChartCalculationFactory factory, Chart chart)
            : base(factory, chart)
        {
        }

        public SKPaint CreateBarPaint(SeriesValue value)
        {
            var calculationFactory = factory as BarChartCalculationFactory;

            var paint = new SKPaint
            {
                Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : palette.GetAvaibleColour(),
                StrokeWidth = calculationFactory.BarSize,
                TextSize = chart.TextSize,
                TextAlign = SKTextAlign.Center
            };

            if (!value.Focused && chart.HasShapeFocused)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                paint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, chart.Sigma);
            }

            return paint;
        }

        public SKRect CreateBarRect(SeriesValue value)
        {
            if(factory is VerticalBarChartCalculationFactory)
            {
                return CreateVerticalBarRect(value);
            }
            else
            {
                return CreateHorizontalBarRect(value);
            }
        }

        public SKRect CreateVerticalBarRect(SeriesValue value)
        {
            var calculationFactory = factory as BarChartCalculationFactory;

            var barStartingPoint = calculationFactory.CalculateBarStartingPoint(chart.Series.IndexOf(value));

            var left = calculationFactory.CalculateLeft(barStartingPoint);
            var right = calculationFactory.CalculateRight(barStartingPoint);
            var bottom = calculationFactory.CalculateBottom();
            var top = calculationFactory.CalculateTop(value);

            var rect = new SKRect(left, top, right, bottom);
            return rect;
        }

        public SKRect CreateHorizontalBarRect(SeriesValue value)
        {
            var calculationFactory = factory as BarChartCalculationFactory;

            var barStartingPoint = calculationFactory.CalculateBarStartingPoint(chart.Series.IndexOf(value));

            var left = calculationFactory.CalculateLeft(barStartingPoint);
            var right = calculationFactory.CalculateRight(value);
            var bottom = calculationFactory.CalculateBottom(barStartingPoint);
            var top = calculationFactory.CalculateTop(barStartingPoint);

            var rect = new SKRect(left, top, right, bottom);
            return rect;
        }

        public SKPoint CreateLabelPointPosition(SeriesValue value)
        {
            var calculationFactory = factory as BarChartCalculationFactory;

            var barStartingPoint = calculationFactory.CalculateBarStartingPoint(chart.Series.IndexOf(value));

            return new SKPoint(barStartingPoint, chart.CanvasSize.Height - 40);
        }

        public SKPoint CreateValuePointPosition(SeriesValue value)
        {
            var calculationFactory = factory as BarChartCalculationFactory;

            var barStartingPoint = calculationFactory.CalculateBarStartingPoint(chart.Series.IndexOf(value));

            return new SKPoint(barStartingPoint, chart.CanvasSize.Height - 10);
        }
    }
}
