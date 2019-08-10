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
        protected float marginY = 80;
        protected float marginX;
        protected float avaibleHeight;
        protected float avaibleWidth;
        protected PaletteColour palette = new PaletteColour();
        protected IEnumerable<SKColor> defaultColours;
        protected List<Shape> shapes = new List<Shape>();
        protected bool shapeTouched;
        protected float sigma;
        protected int fullSigma = 10;

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

        public List<SeriesValue> Series
        {
            get => (List<SeriesValue>)GetValue(SeriesProperty);
            set => SetValue(SeriesProperty, value);
        }

        public float MaxEntryValue => Series.Max(x => Math.Abs(x.Value));

        public bool HasNegativeValues => Series.Any(x => x.Value < 0);

        public bool AllNegatives => Series.All(x => x.Value < 0);

        public bool AllPositive => Series.All(x => x.Value >= 0);

        protected abstract void DrawChart();

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            surface = e.Surface;
            info = e.Info;

            canvas.Clear();

            if (Series.NotNullNorEmpty())
            {
                MeasureMargins();
                MeasureRegionForDrawing();

                defaultColours = palette.GetColours(Series.Count);

                PopulateShapes();

                DrawChart();
            }            
        }

        public virtual void PopulateShapes()
        {
            shapes.Clear();

            shapes.AddRange(Series.Select(x => Create(x)));
        }

        public abstract Shape Create(SeriesValue value);

        static void OnBindablePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (newValue == null) return;

            var chart = ((Chart)sender);

            chart.InvalidateSurface();
        }

        protected SKColor GetDefaultColour(SeriesValue value) => defaultColours.ElementAt(Series.IndexOf(value));

        protected bool UseCaption() => Series.All(x => !string.IsNullOrEmpty(x.Caption));

        protected virtual void MeasureMargins()
        {
            marginY = CanvasSize.Height * 20 / 100;
            marginX = CanvasSize.Width * 5 / 100;
        }

        protected virtual void MeasureRegionForDrawing()
        {
            avaibleHeight = CanvasSize.Height - marginY;
            avaibleWidth = CanvasSize.Width - marginX;
        }

        protected async Task AnimatedDraw()
        {
            sigma = 0;
            for (int i = 0; i < fullSigma; i++)
            {
                await Task.Delay(25);

                sigma += (float)i / 4;
                InvalidateSurface();
            }
        }

        private void Chart_Touch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType.Equals(SKTouchAction.Pressed))
            {
                foreach (var shape in shapes)
                {
                    shape.Focused = shape.ContainsPoint(e.Location);
                }

                shapeTouched = shapes.Any(x => x.Focused);
                if (shapeTouched)
                {
                    Task.Run(async () => await AnimatedDraw());
                }
                else
                {
                    InvalidateSurface();
                }
            }
        }
    }
}
