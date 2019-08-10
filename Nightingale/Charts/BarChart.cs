using Nightingale.Abstract;
using Nightingale.Figures;
using SkiaSharp;

namespace Nightingale.Charts
{
    public class BarChart : LinealChart
    {
        /// <summary>
        /// The space between bars is a quarter of a bar
        /// </summary>
        protected float spaceBetweenBars;

        /// <summary>
        /// Calculate the width of a bar by calculating the total width with how many entries and the space between all bars
        /// </summary>
        protected float barSize;

        public override Shape Create(SeriesValue value)
        {
            float x = (Series.IndexOf(value) * barSize + spaceBetweenBars) * 2;

            var middleWidth = barSize / 2;
            var left = x - middleWidth;
            var right = x + middleWidth;
            var bottom = AxisX;
            var top = DistanceFromAxisX(value);

            var paint = new SKPaint
            {
                Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                StrokeWidth = barSize,
                TextSize = TextSize,
                TextAlign = SKTextAlign.Center
            };

            if (!value.Focused && shapeTouched)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                paint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, sigma);
            }

            var shape = new Bar(canvas)
            {
                Paint = paint,
                Rect = new SKRect(left, top, right, bottom),
                LabelPosition = new SKPoint(x, CanvasSize.Height - 40),
                ValuePosition = new SKPoint(x, CanvasSize.Height - 10),
                SerieValue = value
            };

            return shape;
        }

        protected override void DrawChart()
        {
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }

        protected override void MeasureRegionForDrawing()
        {
            base.MeasureRegionForDrawing();

            spaceBetweenBars = (avaibleWidth / Series.Count) / 4;
            barSize = (avaibleWidth - (spaceBetweenBars * (Series.Count * 2))) / Series.Count;
        }
    }
}
