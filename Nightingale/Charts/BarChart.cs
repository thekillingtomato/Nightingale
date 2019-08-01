using Nightingale.Abstract;
using Nightingale.Shapes;
using SkiaSharp;
using System;
using System.Linq;

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

        protected override void DrawChart()
        {
            spaceBetweenBars = (avaibleWidth / Values.Count) / 4;
            barSize = (avaibleWidth - (spaceBetweenBars * (Values.Count * 2))) / Values.Count;

            foreach (var value in Values)
            {
                float xStartPoint = (Values.IndexOf(value) * barSize + spaceBetweenBars) * 2;

                var barHeight = DistanceFromAxisX(value);

                var paint = new SKPaint
                {
                    Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                    StrokeWidth = barSize,
                    TextSize = TextSize,
                    TextAlign = SKTextAlign.Center
                };

                canvas.DrawLine(new SKPoint(xStartPoint, AxisX), new SKPoint(xStartPoint, barHeight), paint);
                canvas.DrawText(value.Label, new SKPoint(xStartPoint, CanvasSize.Height - 40), paint);
                canvas.DrawText(value.Value.ToString(), new SKPoint(xStartPoint, CanvasSize.Height - 10), paint);
            }
        }
    }
}
