using SkiaSharp;
using System;
using System.Linq;

namespace Nightingale
{
    public abstract class CircularChart : Chart
    {
        public float Radius => Math.Min(CanvasSize.Width / 2.5f, CanvasSize.Height / 2.5f);

        public SKPoint CenterPoint => new SKPoint(CanvasSize.Width / 2, CanvasSize.Height / 2);

        public SKRect CenterRect => new SKRect(CenterPoint.X - Radius, CenterPoint.Y - Radius,
                                                    CenterPoint.X + Radius, CenterPoint.Y + Radius);

        public float StartAngle { get; protected set; }

        protected float GetYAxisFor(ChartValue value)
        {
            var margin = avaibleHeight / (20 / 2);

            var start = (Values.IndexOf(value) / 2) + 1;

            return TextSize * 2 * start;
        }

        protected float GetXAxisFor(ChartValue value)
            => Values.IndexOf(value) % 2 == 0 ?
                    CanvasSize.Width - avaibleWidth :
                    CanvasSize.Width - 20;

        protected void DrawLabel(ChartValue value, SKPaint paint)
        {
            var labelPosition = new SKPoint(GetXAxisFor(value), GetYAxisFor(value));

            var leftSide = Values.IndexOf(value) % 2 == 0;

            var text = leftSide ?
                $"{value.Label} {(UseCaption() ? value.Caption : CalculatePercentage(value).ToString("F0") + "%")}" :
                $"{(UseCaption() ? value.Caption : CalculatePercentage(value).ToString("F0") + "%")} {value.Label}";

            paint.TextSize = TextSize;

            if (leftSide)
            {
                canvas.DrawText(text, labelPosition, paint);
            }
            else
            {
                var width = paint.MeasureText(text);
                canvas.DrawText(text, labelPosition.X - 20 - width, labelPosition.Y, paint);
            }
        }

        protected float CalculatePercentage(ChartValue value) => value.Value * 100 / Values.Sum(x => x.Value);
    }
}
