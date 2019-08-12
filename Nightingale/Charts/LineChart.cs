using Nightingale.Calculations;
using Nightingale.Drawable;
using Nightingale.Figures;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Linq;
using Xamarin.Forms;
using Point = Nightingale.Figures.Point;

namespace Nightingale.Charts
{
    public class LineChart : Chart
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

        internal override MainCalculationFactory CreateFactory() => new LineCalculationFactory(this);

        internal override MainDrawableFactory CreateDrawableFactory() => new LineDrawableFactory(calculationFactory, this);

        protected override void DrawChart()
        {
            foreach (var shape in Shapes)
            {
                var p = shape as Point;
                p.Draw();

                var currentIndex = Shapes.IndexOf(shape);

                if (currentIndex > 0)
                {
                    p.JoinPoints();
                    var previousElement = Shapes.ElementAt(currentIndex - 1) as Point;

                    if (RenderArea)
                    {
                        p.DrawShadowArea(BackgroundColor.ToSKColor(), calculationFactory.AxisX);
                    }
                }
            }
        }

        public override Shape Create(SeriesValue value)
        {
            var factory = calculationFactory as LineCalculationFactory;

            var drawableFactory = mainDrawableFactory as LineDrawableFactory;

            var dotPaint = drawableFactory.CreateDotPaint(value);

            var point = new SKPoint(factory.CalculateBarStartingPoint(Series.IndexOf(value)), factory.CalculateTop(value));

            return new Point(canvas)
            {
                Paint = dotPaint,
                Value = point,
                SerieValue = value,
                LabelPosition = new SKPoint(point.X, CanvasSize.Height - 40),
                ValuePosition = new SKPoint(point.X, CanvasSize.Height - 10),
                Related = Shapes.Count > 0 ? Shapes.Last() as Point : null
            };
        }
    }
}
