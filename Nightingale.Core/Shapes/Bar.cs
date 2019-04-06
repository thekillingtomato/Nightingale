using SkiaSharp;

namespace Nightingale.Core.Shapes
{
    public class Bar : DrawableShape
    {
        public Bar(SKCanvas canvas, SKPoint startingPoint, SKPoint endingPoint, SKPaint paint, ChartValue entry)
            : base(canvas, startingPoint, paint)
        {
            Entry = entry;
            EndingPoint = endingPoint;
        }

        public ChartValue Entry { get; }

        public SKPoint EndingPoint { get; }

        public override void Draw()
        {
            canvas.DrawLine(StartingPoint, EndingPoint, Paint);
            DrawValueLabel();
            DrawEntryLabel();
        }

        private void DrawValueLabel()
        {
            canvas.DrawText(Entry.IsCaptionEmpty() ? Entry.Value.ToString() : Entry.Caption,
                StartingPoint.X - (Paint.StrokeWidth / 2),
                EndingPoint.Y - (Entry.Value > 0 ? 20 : -20),
                Paint);
        }

        private void DrawEntryLabel()
        {
            float textOffset = Paint.StrokeWidth / 2;
            canvas.DrawText(Entry.Label,
                StartingPoint.X - textOffset,
                StartingPoint.Y + (Entry.Value > 0 ? 20 : -20),
                Paint);
        }
    }
}
