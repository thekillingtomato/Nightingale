using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Nightingale.Charts
{
    public class LinearChart : Chart
    {
        protected override void DrawChart()
        {
            canvas.Clear();

            using (var path = new SKPath())
            {
                var paint = new SKPaint
                {
                    StrokeWidth = 20,
                    Style = SKPaintStyle.Stroke
                };

                var colors = new List<SKColor>();

                foreach (var value in Values)
                {
                    var dotPaint = new SKPaint
                    {
                        Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                        StrokeWidth = 20,
                    };

                    colors.Add(dotPaint.Color);

                    var point = CreatePoint(value);
                    canvas.DrawCircle(point, 25, dotPaint);

                    if (Values.IndexOf(value).Equals(0))
                    {
                        path.MoveTo(point);
                        continue;
                    }

                    path.LineTo(point);
                }
                
                paint.Shader = SKShader.CreateLinearGradient(path.GetPoint(0), path.GetPoint(Values.Count - 1),
                    colors.ToArray(), null, SKShaderTileMode.Clamp);

                canvas.DrawPath(path, paint);
            }
        }

        private float CalculateYHeight(ChartValue value)
        {
            var percentage = value.Value / ValuesRatio * 100 / avaibleHeight;
            var increaseHeight = Math.Abs(percentage * (avaibleHeight - marginY) / 100);
            return avaibleHeight - (value.Value > 0 ? increaseHeight : -increaseHeight);
        }

        private SKPoint CreatePoint(ChartValue value)
        {
            var x = (avaibleWidth / Values.Count) * Values.IndexOf(value) + marginX;
            var y = CalculateYHeight(value);

            return new SKPoint(x, y);
        }
    }
}
