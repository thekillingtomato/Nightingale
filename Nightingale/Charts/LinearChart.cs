using Nightingale.Abstract;
using SkiaSharp;
using System.Linq;

namespace Nightingale.Charts
{
    public class LinearChart : LinealChart
    {
        protected override void DrawChart()
        {
            var elements = Values.Select(x => new
            {
                DotPaint = CreateDotPaint(x),
                Point = CreatePoint(x),
                ChartValue = x
            }).ToList();

            using (var path = new SKPath())
            {
                var paint = new SKPaint
                {
                    StrokeWidth = 5,
                    Style = SKPaintStyle.Stroke
                };

                foreach (var element in elements)
                {
                    canvas.DrawCircle(element.Point, 10, element.DotPaint);
                    canvas.DrawText(element.ChartValue.Label, new SKPoint(element.Point.X, CanvasSize.Height - 40), element.DotPaint);
                    canvas.DrawText(element.ChartValue.Value.ToString(), new SKPoint(element.Point.X, CanvasSize.Height - 10), element.DotPaint);

                    if (Values.IndexOf(element.ChartValue).Equals(0))
                    {
                        path.MoveTo(element.Point);
                        continue;
                    }

                    path.LineTo(element.Point);
                }

                paint.Shader = SKShader.CreateLinearGradient(path.GetPoint(0), path.GetPoint(Values.Count - 1),
                    elements.Select(x => x.DotPaint.Color).ToArray(), null, SKShaderTileMode.Clamp);

                canvas.DrawPath(path, paint);
            }
        }
    }
}
