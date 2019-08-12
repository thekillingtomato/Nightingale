using Nightingale.Calculations;
using SkiaSharp;

namespace Nightingale.Drawable
{
    internal class LineDrawableFactory : MainDrawableFactory
    {
        internal LineDrawableFactory(MainCalculationFactory factory, Chart chart) : base(factory, chart)
        {
        }

        public SKPaint CreateDotPaint(SeriesValue value)
        {
            var paint = new SKPaint
            {
                Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : palette.GetAvaibleColour(),
                StrokeWidth = 20,
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
    }
}
