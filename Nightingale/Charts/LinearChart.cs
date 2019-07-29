using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nightingale.Charts
{
    public class LinearChart : Chart
    {
        List<Point> points = new List<Point>();

        protected override void DrawChart()
        {
            canvas.Clear();

            foreach (var value in Values)
            {
                var index = Values.IndexOf(value);

                var paint = new SKPaint
                {
                    Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                    TextSize = TextSize,
                    TextAlign = SKTextAlign.Center,
                    StrokeWidth = 20,
                };

                var x = (avaibleWidth / Values.Count) * index + marginX;
                var y = CalculateYHeight(value);

                var point = new SKPoint(x, y);
                canvas.DrawCircle(point, 5, paint);

                points.Add(new Point { Value = point, Paint = paint });
            }

            using (var path = new SKPath())
            {
                var paint = new SKPaint
                {
                    Color = SKColors.White,
                    StrokeWidth = 2,
                    Style = SKPaintStyle.Stroke
                };

                foreach (var point in points)
                {
                    if (points.IndexOf(point).Equals(0))
                    {
                        path.MoveTo(points.First().Value);
                        continue;
                    }

                    path.LineTo(point.Value);
                    
                    if (!points.IndexOf(point).Equals(0) && !points.IndexOf(point).Equals(points.Count - 1))
                    {
                        var previousPoint = points.ElementAt(points.IndexOf(point) - 1);
                        var skColours = new SKColor[]
                        {
                            previousPoint.Paint.Color,
                            point.Paint.Color
                        };

                        paint.Shader = SKShader.CreateLinearGradient(previousPoint.Value,
                            point.Value,
                            skColours,
                            null,
                            SKShaderTileMode.Mirror);

                        //path.ArcTo(point.Value, previousPoint.Value, 100);
                    }
                    
                    canvas.DrawPath(path, paint);
                }

                //canvas.DrawPath(path, paint);
            }
        }

        private float CalculateYHeight(ChartValue value)
        {
            var percentage = value.Value / ValuesRatio * 100 / avaibleHeight;
            var increaseHeight = Math.Abs(percentage * (avaibleHeight - marginY) / 100);
            return avaibleHeight - (value.Value > 0 ? increaseHeight : -increaseHeight);
        }
    }

    public class Point
    {
        public SKPoint Value { get; set; }

        public SKPaint Paint { get; set; }
    }
}
