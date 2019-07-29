using SkiaSharp;

namespace Nightingale.Shapes
{
    public class ReferenceLabel : DrawableShape
    {
        private string text;
        private bool drawLeftReference;

        public ReferenceLabel(SKCanvas canvas, SKPoint position, SKPaint paint, string text, bool drawLeftReference = false) 
            : base(canvas, position, paint)
        {
            this.text = text;
            this.drawLeftReference = drawLeftReference;
        }

        public override void Draw()
        {
            if (drawLeftReference)
            {
                canvas.DrawText(text, StartingPoint, Paint);
            }
            else
            {
                var width = Paint.MeasureText(text);
                canvas.DrawText(text, StartingPoint.X - 20 - width, StartingPoint.Y, Paint);
            }
        }
    }
}
