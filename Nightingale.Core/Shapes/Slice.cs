﻿using SkiaSharp;

namespace Nightingale.Core.Shapes
{
    public class Slice : DrawableShape
    {
        private SKRect rect;
        private SKPoint center;

        public Slice(SKCanvas canvas, SKPaint paint, SKRect rect, SKPoint center,float startingAngle, float endingAngle) 
            : base(canvas, SKPoint.Empty, paint)
        {
            StartingAngle = startingAngle;
            EndingAngle = endingAngle;
            this.rect = rect;
            this.center = center;
        }

        public float StartingAngle { get; }
        public float EndingAngle { get; }

        public override void Draw()
        {
            using (SKPath path = new SKPath())
            {
                path.MoveTo(center);
                path.ArcTo(rect, StartingAngle, EndingAngle, false);
                path.Close();
                Paint.Style = SKPaintStyle.Fill;
                canvas.DrawPath(path, Paint);
            }
        }
    }
}
