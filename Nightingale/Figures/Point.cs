using SkiaSharp;
using System;

namespace Nightingale.Figures
{
    public class Point : Shape
    {
        public Point(SKCanvas canvas) : base(canvas)
        {
        }

        public SKPoint Value { get; set; }

        public Point Related { get; set; }

        public override bool ContainsPoint(SKPoint point)
        {
            var diffX = Math.Abs(point.X - Value.X);
            var diffY = Math.Abs(point.Y - Value.Y);

            return diffX < 10 && diffY < 10;
        }

        public override void Draw()
        {
            canvas.DrawCircle(Value, 10, Paint);

            canvas.DrawText(SerieValue.Label,
                LabelPosition,
                Paint);

            canvas.DrawText(SerieValue.Value.ToString(),
                ValuePosition,
                Paint);
        }

        public void DrawLines()
        {
            if (Related == null) return;

            using (var path = new SKPath())
            {
                path.AddPoly(new SKPoint[] { Related.Value, Value });
                var currentColors = new SKColor[]
                {
                                Related.Paint.Color,
                                Paint.Color
                };

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 5,
                    Shader = SKShader.CreateLinearGradient(Related.Value, Value,
                                            currentColors,
                                            null, SKShaderTileMode.Clamp),
                    MaskFilter = Paint.MaskFilter
                };

                canvas.DrawPath(path, paint);
            }
        }

        public void DrawShadowArea(SKColor backgroundColor, float axisX)
        {
            using (var path = new SKPath())
            {
                var p0 = new SKPoint(Related.Value.X, axisX);
                var p1 = new SKPoint(Value.X, axisX);

                path.AddPoly(new SKPoint[] { Related.Value, p0, p1, Value });

                var shadowPaint = new SKPaint
                {
                    Color = new SKColor(backgroundColor.Red.ChangeBy(5),
                                            backgroundColor.Green.ChangeBy(5),
                                            backgroundColor.Blue.ChangeBy(5)),
                    BlendMode = SKBlendMode.Lighten
                };
                shadowPaint.MaskFilter = Paint.MaskFilter;

                canvas.DrawPath(path, shadowPaint);
            }
        }
    }
}
