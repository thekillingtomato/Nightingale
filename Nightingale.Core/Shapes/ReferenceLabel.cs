using SkiaSharp;

namespace Nightingale.Core.Shapes
{
    public class ReferenceLabel : DrawableShape
    {
        private string text;

        public ReferenceLabel(SKCanvas canvas, SKPoint position, SKPaint paint, string text) 
            : base(canvas, position, paint)
        {
            this.text = text;
        }

        public override void Draw()
        {
            canvas.DrawText(text, StartingPoint, Paint);
            canvas.DrawCircle(StartingPoint.X - 10,
                StartingPoint.Y - 5,
                5,
                new SKPaint
                {
                    Color = Paint.Color,
                });
        }
    }
}
