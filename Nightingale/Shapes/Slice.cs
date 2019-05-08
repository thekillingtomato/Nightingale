using SkiaSharp;

namespace Nightingale.Shapes
{
    public class Slice : DrawableShape
    {
        private SKRect rect;

        public Slice(SKCanvas canvas, SKPaint paint, SKRect rect, SKPoint center,float startingAngle, float endingAngle) 
            : base(canvas, center, paint)
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
                path.MoveTo(StartingPoint);
                path.ArcTo(rect, StartingAngle, EndingAngle, false);
                path.Close();
                Paint.Style = SKPaintStyle.Fill;
                canvas.DrawPath(path, Paint);
            }
        }
    }
}
