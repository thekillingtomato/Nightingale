﻿using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Nightingale.Core
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

        public float TextSize { get; set; } = 12;

        public List<ChartValue> Entries { get; set; }

        public float MaxEntryValue => Entries.Max(x => x.Value);

        public float ValuesRatio => Entries.Max(x => x.Value) > avaibleHeight ? Entries.Max(x => x.Value) / avaibleHeight : 1;

        protected abstract void DrawChart();

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            surface = e.Surface;
            info = e.Info;
            
            marginY = CanvasSize.Height * 20 / 100;
            marginX = CanvasSize.Width * 5 / 100;
            avaibleHeight = CanvasSize.Height - marginY;
            avaibleWidth = CanvasSize.Width  - marginX;

            DrawChart();
        }
    }
}
