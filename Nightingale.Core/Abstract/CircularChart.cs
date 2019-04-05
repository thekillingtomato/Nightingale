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
    }
}
