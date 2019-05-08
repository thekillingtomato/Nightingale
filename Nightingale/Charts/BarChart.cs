using Nightingale.Shapes;
using SkiaSharp;
using System;
using System.Linq;

namespace Nightingale.Charts
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
            // For negative values.
            if(Values.Any(x => x.Value < 0))
            {
                avaibleHeight = avaibleHeight / 2;
            }

            spaceBetweenBars = (avaibleWidth / Values.Count) / 4;
            barSize = (avaibleWidth - (spaceBetweenBars * (Values.Count * 2))) / Values.Count;

            foreach (var value in Values)
            {
                float xStartPoint = (Values.IndexOf(value) * barSize + spaceBetweenBars) * 2;

                var barHeight = CalculateYHeight(value);

                var paint = new SKPaint
                {
                    Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : palette.GetAvaibleColour(),
                    StrokeWidth = barSize,
                    TextSize = TextSize
                };
                var bar = new Bar(canvas, new SKPoint(xStartPoint, avaibleHeight), new SKPoint(xStartPoint, barHeight), paint, value);                

                bar.Draw();
            }
        }

        /// <summary>
        /// Measures the value to be draw
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float CalculateYHeight(ChartValue value)
        {
            var percentage = value.Value / ValuesRatio * 100 / avaibleHeight;
            var increaseHeight = Math.Abs(percentage * (avaibleHeight - marginY) / 100);
            return avaibleHeight - (value.Value > 0 ? increaseHeight : -increaseHeight);
        }
    }
}
