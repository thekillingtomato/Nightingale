using Nightingale.Abstract;
using Nightingale.Figures;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Linq;
using Xamarin.Forms;
using Point = Nightingale.Figures.Point;

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
            foreach (var shape in shapes)
            {
                var p = shape as Point;
                p.Draw();

                var currentIndex = shapes.IndexOf(shape);

                if (currentIndex > 0)
                {
                    p.DrawLines();
                    var previousElement = shapes.ElementAt(currentIndex - 1) as Point;

                    if (RenderArea)
                    {
                        p.DrawShadowArea(BackgroundColor.ToSKColor(), AxisX);
                    }
                }
            }
        }

        public override Shape Create(SeriesValue value)
        {
            var dotPaint = CreateDotPaint(value);
            var point = CreatePoint(value);

            if (!value.Focused && shapeTouched)
            {
                SKBlurStyle blurStyle = SKBlurStyle.Normal;
                dotPaint.MaskFilter = SKMaskFilter.CreateBlur(blurStyle, sigma);
            }

            return new Point(canvas)
            {
                Paint = dotPaint,
                Value = point,
                SerieValue = value,
                LabelPosition = new SKPoint(point.X, CanvasSize.Height - 40),
                ValuePosition = new SKPoint(point.X, CanvasSize.Height - 10),
                Related = shapes.Count > 0 ? shapes.Last() as Point : null
            };
        }
    }
}
