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
            //using (var path = new SKPath())
            //{
            //    path.FillType = SKPathFillType.Winding;

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

                if (currentIndex.Equals(0))
                {
                    //path.MoveTo(point);
                }
                else
                {
                    var previous = points.ElementAt(currentIndex - 1);

                    using (var path = new SKPath())
                    {
                        path.FillType = SKPathFillType.Winding;
                        
                        var p0 = new SKPoint(previous.X, AxisX);
                        var p1 = new SKPoint(point.X, AxisX);

                        path.AddPoly(new SKPoint[] { previous, p0, p1, point });

                        paint.Shader = SKShader.CreateLinearGradient(p0, p1,
                                            new SKColor[] { colors.ElementAt(currentIndex - 1), colors.ElementAt(currentIndex) },
                                            null, SKShaderTileMode.Clamp);

                        canvas.DrawPath(path, paint);
                    }

                    using(var linePath = new SKPath())
                    {
                        linePath.MoveTo(previous);
                        linePath.LineTo(point);

                        var background = BackgroundColor.ToSKColor();
                        var inverse = new SKColor((byte)(byte.MaxValue - background.Red),
                                                    (byte)(byte.MaxValue - background.Green),
                                                    (byte)(byte.MaxValue - background.Blue));

                        //canvas.DrawPath(linePath, new SKPaint
                        //{
                        //    StrokeWidth = 1,
                        //    Color = inverse,
                        //    Style = SKPaintStyle.Stroke
                        //});
                    }

                }
                points.Add(point);
            }

            //paint.Shader = SKShader.CreateLinearGradient(path.GetPoint(1), path.GetPoint(Values.Count - 1),
            //    colors.ToArray(), null, SKShaderTileMode.Clamp);

            //canvas.DrawPath(path, paint);
            //}
        }

        private SKPoint CreatePoint(ChartValue value)
        {
            var x = MeasureX(value);
            var y = DistanceFromAxisX(value);

            return new SKPoint(x, y);
        }

        private float MeasureX(ChartValue value)
        {
            var x = (avaibleWidth / Values.Count) * Values.IndexOf(value) + marginX;
            return x;
        }
    }
}
