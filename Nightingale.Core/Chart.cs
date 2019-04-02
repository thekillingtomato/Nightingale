using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Nightingale.Core
{
    public abstract class Chart : SKCanvasView
    {
        protected SKCanvas canvas;
        protected float marginY = 80;
        protected float avaibleHeight;
        protected float avaibleWidth;
        protected PaletteColour palette = new PaletteColour();

        public Chart()
        {
            BackgroundColor = Color.Transparent;
            PaintSurface += OnPaintSurface;
        }

        public List<ChartValue> Entries { get; set; }

        public float MaxEntryValue => Entries.Max(x => x.Value);

        public float ValuesRatio => Entries.Max(x => x.Value) > avaibleHeight ? Entries.Max(x => x.Value) / avaibleHeight : 1;

        protected abstract void DrawChart();

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            
            marginY = CanvasSize.Height * 20 / 100;
            avaibleHeight = CanvasSize.Height - marginY;
            avaibleWidth = CanvasSize.Width;

            DrawChart();
        }
    }
}
