using Nightingale.Core.Shapes;
using SkiaSharp;
using System;

namespace Nightingale.Core
{
    public abstract class CircularChart : Chart
    {
        public float Radius => Math.Min(CanvasSize.Width / 2.5f, CanvasSize.Height / 2.5f);

        public SKPoint CenterPoint => new SKPoint(CanvasSize.Width / 2, CanvasSize.Height / 2);

        public SKRect CenterRect => new SKRect(CenterPoint.X - Radius, CenterPoint.Y - Radius,
                                                    CenterPoint.X + Radius, CenterPoint.Y + Radius);

        public float StartAngle { get; protected set; }

        protected float GetYAxisFor(ChartValue value)
            => CanvasSize.Height - (CanvasSize.Height - 10) + (Values.IndexOf(value) + (1 - Values.IndexOf(value) % 2)) * 20;

        protected float GetXAxisFor(ChartValue value)
            => Values.IndexOf(value) % 2 == 0 ?
                    CanvasSize.Width - avaibleWidth :
                    CanvasSize.Width - 20;

        protected void DrawLabel(ChartValue value, SKPaint paint)
        {
            var labelPosition = new SKPoint(GetXAxisFor(value), GetYAxisFor(value));

            var text = $"{value.Label} [{(value.IsCaptionEmpty() ? value.Value.ToString() : value.Caption)}]";

            var reference = new ReferenceLabel(canvas, labelPosition, paint, text, Values.IndexOf(value) % 2 == 0);
            reference.Draw();
        }
    }
}
