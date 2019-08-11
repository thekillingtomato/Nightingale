using SkiaSharp;

namespace Nightingale.Figures
{
    internal class Arc : Shape
    {
        public Arc(SKCanvas canvas) : base(canvas)
        {
        }

        public SKRect CenterRect { get; set; }

        public float StartAngle { get; set; }

        public float SweepAngle { get; set; }

        public string Text { get; set; }

        public SKPaint TextPaint { get; set; }

        public SKPath Path { get; private set; } = new SKPath();

        public override bool ContainsPoint(SKPoint point) => Path.Contains(point.X, point.Y);

        public override void Draw()
        {
            Path.AddArc(CenterRect, StartAngle, SweepAngle);
            canvas.DrawPath(Path, Paint);
            canvas.DrawPath(Path, Paint);
            canvas.DrawText(Text, LabelPosition, TextPaint);
        }
    }
}
