using SkiaSharp;

namespace Nightingale.Core.Shapes
{
    public abstract class DrawableShape
    {
        protected SKCanvas canvas;

        public DrawableShape(SKCanvas canvas, SKPoint x, SKPoint y, SKPaint paint)
        {
            this.canvas = canvas;
            X = x;
            Y = y;
            Paint = paint;
        }

        public SKPoint X { get; }

        public SKPoint Y { get; }

        public SKPaint Paint { get; }

        public virtual void Draw()
        {
            canvas.DrawLine(X, Y, Paint);
        }
    }
}
