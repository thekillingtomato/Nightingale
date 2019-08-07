using Nightingale.Abstract;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Linq;
using Xamarin.Forms;

namespace Nightingale.Charts
{
    public class LineChart : LinealChart
    {
        public bool RenderArea
        {
            get => (bool)GetValue(RenderAreaProperty);
            set => SetValue(RenderAreaProperty, value);
        }

        public static readonly BindableProperty RenderAreaProperty =
            BindableProperty.Create(nameof(RenderArea), typeof(bool), typeof(Chart), false, propertyChanged: (BindableObject sender, object oldValue, object newValue) =>
            {

            });

        protected override void DrawChart()
        {
            var elements = Series.Select(x => new
            {
                DotPaint = base.CreateDotPaint(x),
                Point = base.CreatePoint(x),
                SeriesValue = x
            }).ToList();

            foreach (var element in elements)
            {
                var currentIndex = elements.IndexOf(element);

                canvas.DrawCircle(element.Point, 10, element.DotPaint);

                canvas.DrawText(element.SeriesValue.Label, 
                    new SKPoint(element.Point.X, CanvasSize.Height - 40),
                    element.DotPaint);

                canvas.DrawText(element.SeriesValue.Value.ToString(),
                    new SKPoint(element.Point.X, CanvasSize.Height - 10),
                    element.DotPaint);

                if (currentIndex > 0)
                {
                    var previousElement = elements.ElementAt(currentIndex - 1);

                    using (var path = new SKPath())
                    {
                        path.AddPoly(new SKPoint[] { previousElement.Point, element.Point });
                        var currentColors = new SKColor[] 
                        {
                            previousElement.DotPaint.Color,
                            element.DotPaint.Color
                        };

                        canvas.DrawPath(path, CreateStrokePaint(currentColors, previousElement.Point, element.Point));
                    }

                    if (RenderArea)
                    {
                        using (var path = new SKPath())
                        {
                            var p0 = new SKPoint(previousElement.Point.X, AxisX);
                            var p1 = new SKPoint(element.Point.X, AxisX);

                            path.AddPoly(new SKPoint[] { previousElement.Point, p0, p1, element.Point });

                            var backgroundColor = BackgroundColor.ToSKColor();

                            canvas.DrawPath(path, new SKPaint
                            {
                                Color = new SKColor(backgroundColor.Red.ChangeBy(10),
                                                        backgroundColor.Green.ChangeBy(10),
                                                        backgroundColor.Blue.ChangeBy(10)),
                                BlendMode = SKBlendMode.Lighten
                            });
                        }
                    }
                }
            }
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
    }
}
