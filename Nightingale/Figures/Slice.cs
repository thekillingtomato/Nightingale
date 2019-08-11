using SkiaSharp;

namespace Nightingale.Figures
{
    internal class Slice : Shape
    {
        public Slice(SKCanvas canvas) : base(canvas)
        {
            Path = new SKPath();
        }

        public SKRect CenterRect { get; set; }

        public SKPoint Center { get; set; }

        public float StartAngle { get; set; }

        public float SweepAngle { get; set; }

        public string Text { get; set; }

        public SKPaint TextPaint { get; set; }

        public SKPath Path { get; private set; }

        public override bool ContainsPoint(SKPoint point) => Path.Contains(point.X, point.Y);

        public override void Draw()
        {
            Path.MoveTo(Center);
            Path.ArcTo(CenterRect, StartAngle, SweepAngle, false);
            Path.Close();
            canvas.DrawPath(Path, Paint);
            canvas.DrawText(Text, LabelPosition, TextPaint);
        }
    }
}
