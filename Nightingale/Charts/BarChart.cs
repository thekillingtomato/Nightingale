﻿using Nightingale.Abstract;
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

                var barHeight = DistanceFromAxisY(value);

                var paint = new SKPaint
                {
                    Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                    StrokeWidth = barSize,
                    TextSize = TextSize,
                    TextAlign = SKTextAlign.Center
                };
                var bar = new Bar(canvas, new SKPoint(xStartPoint, AxisY), new SKPoint(xStartPoint, barHeight), paint, value);                

                bar.Draw();
            }
        }
    }
}
