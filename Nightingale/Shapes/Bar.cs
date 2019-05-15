using SkiaSharp;

namespace Nightingale.Shapes
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
                StartingPoint.X,
                EndingPoint.Y - (Entry.Value > 0 ? Paint.TextSize : -Paint.TextSize),
                Paint);
        }

        private void DrawEntryLabel()
        {
            var yOffset = Paint.TextSize + 20;
            canvas.DrawText(Entry.Label,
                StartingPoint.X,
                StartingPoint.Y + (Entry.Value > 0 ? yOffset : -yOffset),
                Paint);
        }
    }
}
