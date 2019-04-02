using SkiaSharp;

namespace Nightingale.Core.Shapes
{
    public class Bar : DrawableShape
    {
        public Bar(SKCanvas canvas, SKPoint x, SKPoint y, SKPaint paint ,ChartValue entry)
            : base(canvas, x, y, paint)
        {
            Entry = entry;
        }

        public ChartValue Entry { get; }

        public override void Draw()
        {
            base.Draw();
            DrawValueLabel();
            DrawEntryLabel();
        }

        private void DrawValueLabel()
        {
            canvas.DrawText(Entry.IsCaptionEmpty() ? Entry.Value.ToString() : Entry.Caption , X.X - (Paint.StrokeWidth / 2), Y.Y - 20, Paint);
        }

        private void DrawEntryLabel()
        {
            float textOffset = Paint.StrokeWidth / 2;
            canvas.DrawText(Entry.Label, X.X - textOffset, X.Y + 20, Paint);
        }
    }
}
