using Nightingale.Calculations;
using SkiaSharp;

namespace Nightingale.Drawable
{
    internal class DoughnutDrawableFactory : MainDrawableFactory
    {
        internal DoughnutDrawableFactory(MainCalculationFactory factory, Chart chart) : base(factory, chart)
        {
        }

        public SKPaint CreateArcPaint(SeriesValue seriesValue)
        {
            var paint = new SKPaint
            {
                StrokeWidth = 40,
                Style = SKPaintStyle.Stroke,
                Color = seriesValue.HasColour() ? seriesValue.Colour : palette.GetAvaibleColour(),
            };

            if (!seriesValue.Focused && chart.HasShapeFocused)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                paint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, chart.Sigma);
            }

            return paint;
        }

        public SKPaint CreateTextPaint(SeriesValue seriesValue, SKColor color)
        {
            var textPaint = new SKPaint
            {
                Color = color,
                TextSize = chart.TextSize
            };

            if (!seriesValue.Focused && chart.HasShapeFocused)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                textPaint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, chart.Sigma);
            }

            return textPaint;
        }

        public SKPoint CreateLabelPoint(SeriesValue seriesValue, SKPaint textPaint, string text)
        {
            var calculationFactory = factory as PieChartCalculationFactory;

            var labelPosition = new SKPoint(calculationFactory.CalculateLabelXAxis(seriesValue), calculationFactory.CalculateLabelYAxis(seriesValue));
            var leftSide = chart.Series.IndexOf(seriesValue) % 2 == 0;

            if (!leftSide)
            {
                var width = textPaint.MeasureText(text);
                labelPosition = new SKPoint(labelPosition.X - width, labelPosition.Y);
            }

            return labelPosition;
        }
    }
}