using SkiaSharp;

namespace Nightingale.Core.Shapes
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
                canvas.DrawCircle(StartingPoint.X - 10,
                    StartingPoint.Y - 5,
                    5,
                    new SKPaint
                    {
                        Color = Paint.Color,
                    });
            }
            else
            {
                var width = Paint.MeasureText(text);
                canvas.DrawText(text, StartingPoint.X - 20 - width, StartingPoint.Y, Paint);
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
}
