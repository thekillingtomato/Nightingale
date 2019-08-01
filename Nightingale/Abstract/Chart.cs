using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Chart()
        {
            BackgroundColor = Color.Transparent;
            PaintSurface += OnPaintSurface;
        }

        public static readonly BindableProperty ValuesProperty =
            BindableProperty.Create(nameof(Values), typeof(List<ChartValue>), typeof(Chart), new List<ChartValue>(), propertyChanged: OnBindablePropertyChanged);

        public float TextSize { get; set; } = 12;

        public List<ChartValue> Values
        {
            get => (List<ChartValue>)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

        public float MaxEntryValue => Values.Max(x => Math.Abs(x.Value));

        public bool HasNegativeValues => Values.Any(x => x.Value < 0);

        public bool AllNegatives => Values.All(x => x.Value < 0);

        public bool AllPositive => Values.All(x => x.Value >= 0);

        protected abstract void DrawChart();

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            surface = e.Surface;
            info = e.Info;

            if (Values.NotNullNorEmpty())
            {
                MeasureMargins();
                MeasureRegionForDrawing();

                defaultColours = palette.GetColours(Values.Count);

                DrawChart();
            }
        }

        static void OnBindablePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Chart)sender).InvalidateSurface();
        }

        protected SKColor GetDefaultColour(ChartValue value) => defaultColours.ElementAt(Values.IndexOf(value));

        protected bool UseCaption() => Values.All(x => !string.IsNullOrEmpty(x.Caption));

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
    }
}
