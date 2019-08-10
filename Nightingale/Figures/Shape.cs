using SkiaSharp;

namespace Nightingale.Figures
{
    public abstract class Shape
    {
        protected SKCanvas canvas;
        bool focused;

        public Shape(SKCanvas canvas)
        {
            this.canvas = canvas;
        }

        public SeriesValue SerieValue { get; set; }

        public SKPaint Paint { get; set; }

        public SKPoint LabelPosition { get; set; }

        public SKPoint ValuePosition { get; set; }

        public bool Focused
        {
            get => focused;
            set
            {
                focused = value;
                SerieValue.Focused = value;
            }
        }

        public virtual bool ContainsPoint(SKPoint point) => false;

        public abstract void Draw();
    }
}
