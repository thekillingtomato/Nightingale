using Nightingale.Abstract;
using SkiaSharp;
using System.Collections.Generic;

namespace Nightingale.Charts
{
    public class LinearChart : LinealChart
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
                        TextSize = TextSize,
                        TextAlign = SKTextAlign.Center
                    };

                    colors.Add(dotPaint.Color);

                    var point = CreatePoint(value);
                    canvas.DrawCircle(point, 25, dotPaint);

                    canvas.DrawText(value.Label, new SKPoint(point.X, CanvasSize.Height - 40), dotPaint);
                    canvas.DrawText(value.Value.ToString(), new SKPoint(point.X, CanvasSize.Height - 10), dotPaint);

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

        private SKPoint CreatePoint(ChartValue value)
        {
            var x = (avaibleWidth / Values.Count) * Values.IndexOf(value) + marginX;
            var y = DistanceFromAxisX(value);

            return new SKPoint(x, y);
        }
    }
}
