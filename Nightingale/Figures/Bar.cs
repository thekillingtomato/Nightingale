using SkiaSharp;

namespace Nightingale.Figures
{
    internal class Bar : Shape
    {
        public Bar(SKCanvas canvas) : base(canvas)
        {
        }

        public SKRect Rect { get; set; }

        public override bool ContainsPoint(SKPoint point) => Rect.Standardized.Contains(point);

        public override void Draw()
        {
            canvas.DrawRect(Rect, Paint);
            canvas.DrawText(SerieValue.Label, LabelPosition, Paint);
            canvas.DrawText(SerieValue.Value.ToString(), ValuePosition, Paint);
        }
    }
}
