using SkiaSharp;

namespace Nightingale.Core.Shapes
{
    public abstract class DrawableShape
    {
        protected SKCanvas canvas;

        public DrawableShape(SKCanvas canvas, SKPoint startingPoint, SKPaint paint)
        {
            this.canvas = canvas;
            StartingPoint = startingPoint;
            Paint = paint;
        }

        public SKPoint StartingPoint { get; }

        public SKPaint Paint { get; }

        public abstract void Draw();
    }
}
