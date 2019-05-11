using SkiaSharp;
using SkiaSharp.Views.Forms;
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

        public float MaxEntryValue => Values.Max(x => x.Value);

        public float ValuesRatio => Values.Max(x => x.Value) / avaibleHeight;

        protected abstract void DrawChart();

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            surface = e.Surface;
            info = e.Info;

            marginY = CanvasSize.Height * 20 / 100;
            marginX = CanvasSize.Width * 5 / 100;
            avaibleHeight = CanvasSize.Height - marginY;
            avaibleWidth = CanvasSize.Width - marginX;

            if(Values.NotNullNorEmpty())
            {
                DrawChart();
            }
        }

        static void OnBindablePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Chart)sender).InvalidateSurface();
        }
    }
}
