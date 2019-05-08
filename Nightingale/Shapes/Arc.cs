using SkiaSharp;

namespace Nightingale.Shapes
{
    public class Arc : DrawableShape
    {
        private SKRect rect;

        public Arc(SKCanvas canvas, SKPaint paint, SKRect rect, float startingAngle, float endingAngle)
            : base(canvas, SKPoint.Empty, paint)
        {
            StartingAngle = startingAngle;
            EndingAngle = endingAngle;
            this.rect = rect;
        }

        public float StartingAngle { get; }
        public float EndingAngle { get; }

        public override void Draw()
        {
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, StartingAngle, EndingAngle);
                canvas.DrawPath(path, Paint);
            }
        }
    }
}
