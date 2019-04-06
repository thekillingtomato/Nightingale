using Nightingale.Core.Shapes;
using SkiaSharp;
using System;
using System.Linq;

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
            if(Entries.Any(x => x.Value < 0))
            {
                avaibleHeight = avaibleHeight / 2;
            }

            spaceBetweenBars = (avaibleWidth / Entries.Count) / 4;
            barSize = (avaibleWidth - (spaceBetweenBars * (Entries.Count * 2))) / Entries.Count;
            TextSize = Entries.Any(x => x.Label.Length > 10) ? 10 : 12;

            foreach (var entry in Entries)
            {
                float xStartPoint = (Entries.IndexOf(entry) * barSize + spaceBetweenBars) * 2;

                var barHeight = CalculateYHeight(entry);

                var paint = new SKPaint
                {
                    Color = !entry.Colour.Equals(SKColor.Empty) ? entry.Colour : palette.GetAvaibleColour(),
                    StrokeWidth = barSize,
                    TextSize = TextSize
                };
                var bar = new Bar(canvas, new SKPoint(xStartPoint, avaibleHeight), new SKPoint(xStartPoint, barHeight), paint, entry);                

                bar.Draw();
            }
        }

        /// <summary>
        /// Measures the value to be draw
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        private float CalculateYHeight(ChartValue entry)
        {
            var percentage = entry.Value / ValuesRatio * 100 / avaibleHeight;
            var increaseHeight = Math.Abs(percentage * (avaibleHeight - marginY) / 100);
            return avaibleHeight - (entry.Value > 0 ? increaseHeight : -increaseHeight);
        }
    }
}
