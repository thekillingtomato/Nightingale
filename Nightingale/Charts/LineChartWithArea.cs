using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nightingale.Charts
{
    public class LineChartWithArea : LinearChart
    {
        protected override void DrawChart()
        {
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill
            };
            var points = new List<SKPoint>();
            var colors = new List<SKColor>();

            foreach (var value in Values)
            {
                var currentIndex = Values.IndexOf(value);

                var dotPaint = CreateDotPaint(value);

                colors.Add(dotPaint.Color);

                var point = CreatePoint(value);
                canvas.DrawCircle(point, 10, dotPaint);

                canvas.DrawText(value.Label, new SKPoint(point.X, CanvasSize.Height - 40), dotPaint);
                canvas.DrawText(value.Value.ToString(), new SKPoint(point.X, CanvasSize.Height - 10), dotPaint);

                if (currentIndex > 0)
                {
                    var previousPoint = points.ElementAt(currentIndex - 1);

                    using (var path = ConcatLines(point, previousPoint))
                    {
                        canvas.DrawPath(path, FillShadowPaint());
                    }

                    using (var path = new SKPath())
                    {
                        path.AddPoly(new SKPoint[] { previousPoint, point });
                        var currentColors = new SKColor[] { colors.ElementAt(currentIndex - 1), colors.ElementAt(currentIndex) };

                        canvas.DrawPath(path, CreateStrokePaint(currentColors, previousPoint, point));
                    }
                }
                points.Add(point);
            }
        }

        private float MeasureX(ChartValue value)
        {
            var x = (avaibleWidth / Values.Count) * Values.IndexOf(value) + marginX;
            return x;
        }

        private SKPaint CreateStrokePaint(SKColor[] colors, SKPoint previousPoint, SKPoint point)
        {
            return new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5,
                Shader = SKShader.CreateLinearGradient(previousPoint, point,
                                            colors,
                                            null, SKShaderTileMode.Clamp)
            };
        }

        private SKPath ConcatLines(SKPoint current, SKPoint previous)
        {
            var path = new SKPath();
            var p0 = new SKPoint(previous.X, AxisX);
            var p1 = new SKPoint(current.X, AxisX);

            path.AddPoly(new SKPoint[] { previous, p0, p1, current });

            return path;
        }

        private SKPaint FillShadowPaint()
        {
            var backgroundColor = BackgroundColor.ToSKColor();
            var diff = (byte)(backgroundColor.Red + 20);

            return new SKPaint
            {
                Color = new SKColor(diff, diff, diff),
                BlendMode = SKBlendMode.Lighten
            };
        }

    }
}
