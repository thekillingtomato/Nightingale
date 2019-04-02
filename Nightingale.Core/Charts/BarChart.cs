using Nightingale.Core.Shapes;
using SkiaSharp;
using System;

namespace Nightingale.Core.Charts
{
    public class BarChart : Chart
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
            spaceBetweenBars = (avaibleWidth / Entries.Count) / 4;
            barSize = (avaibleWidth - (spaceBetweenBars * (Entries.Count * 2))) / Entries.Count;

            foreach (var entry in Entries)
            {
                float xStartPoint = (Entries.IndexOf(entry) * barSize + spaceBetweenBars) * 2;

                var barHeight = CalculateYHeight(GetYPercentage(entry));

                var paint = new SKPaint
                {
                    Color = !entry.Colour.Equals(SKColor.Empty) ? entry.Colour : palette.GetAvaibleColour(),
                    StrokeWidth = barSize,
                    TextSize = 16
                };
                var bar = new Bar(canvas, new SKPoint(xStartPoint, avaibleHeight), new SKPoint(xStartPoint, barHeight), paint, entry);                

                bar.Draw();
            }
        }


        /// <summary>
        /// Get the percentage to draw the bar from the bottom to the top
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private float GetYPercentage(ChartValue entry) => entry.Value / ValuesRatio * 100 / avaibleHeight;

        /// <summary>
        /// Measures the value to be draw
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        private float CalculateYHeight(float percentage)
        {
            var increaseHeight = Math.Abs(percentage * (avaibleHeight - marginY) / 100);
            return avaibleHeight - increaseHeight;
        }
    }
}
