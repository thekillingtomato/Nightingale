using Nightingale.Calculations;
using Nightingale.Drawable;
using Nightingale.Figures;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nightingale
{
    public abstract class Chart : SKCanvasView
    {
        protected SKCanvas canvas;
        protected SKImageInfo info;
        protected SKSurface surface;

        protected int fullSigma = 10;
        internal MainCalculationFactory calculationFactory;
        internal MainDrawableFactory mainDrawableFactory;

        public Chart()
        {
            BackgroundColor = Color.Transparent;
            PaintSurface += OnPaintSurface;
            EnableTouchEvents = true;
            Touch += Chart_Touch;
        }

        public static readonly BindableProperty SeriesProperty =
            BindableProperty.Create(nameof(Series), typeof(List<SeriesValue>), typeof(Chart), new List<SeriesValue>(), propertyChanged: OnBindablePropertyChanged);

        public float TextSize { get; set; } = 12;

        public float Sigma { get; private set; } = 0;

        internal List<Shape> Shapes = new List<Shape>();

        public List<SeriesValue> Series
        {
            get => (List<SeriesValue>)GetValue(SeriesProperty);
            set => SetValue(SeriesProperty, value);
        }

        public float MaxEntryValue => Series.Max(x => Math.Abs(x.Value));

        public bool HasNegativeValues => Series.Any(x => x.Value < 0);

        public bool AllNegatives => Series.All(x => x.Value < 0);

        public bool AllPositive => Series.All(x => x.Value >= 0);

        public bool HasShapeFocused => Series.Any(x => x.Focused);

        protected bool UseCaption() => Series.All(x => !string.IsNullOrEmpty(x.Caption));

        protected abstract void DrawChart();

        public abstract Shape Create(SeriesValue value);

        internal virtual MainCalculationFactory CreateFactory() => new MainCalculationFactory(this);

        internal virtual MainDrawableFactory CreateDrawableFactory() => new MainDrawableFactory(calculationFactory, this);

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            surface = e.Surface;
            info = e.Info;

            canvas.Clear();

            Shapes.Clear();

            if (Series.NotNullNorEmpty())
            {
                calculationFactory = CreateFactory();

                mainDrawableFactory = CreateDrawableFactory();

                Shapes.AddRange(Series.Select(x => Create(x)));

                DrawChart();
            }            
        }

        protected async Task AnimatedBlur()
        {
            Sigma = 0;
            for (int i = 0; i < fullSigma; i++)
            {
                await Task.Delay(25);

                Sigma += (float)i / 4;
                InvalidateSurface();
            }
        }

        private void Chart_Touch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType.Equals(SKTouchAction.Pressed))
            {
                foreach (var shape in Shapes)
                {
                    shape.Focused = shape.ContainsPoint(e.Location);
                }

                if (Shapes.Any(x => x.Focused))
                {
                    Task.Run(async () => await AnimatedBlur());
                }
                else
                {
                    InvalidateSurface();
                }
            }
        }

        static void OnBindablePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (newValue == null) return;

            var chart = (Chart)sender;

            chart.InvalidateSurface();
        }
    }
}
