using Nightingale.Core.Shapes;
using SkiaSharp;
using System.Linq;

namespace Nightingale.Core.Charts
{
    public class PieChart : CircularChart
    {
        protected override void DrawChart()
        {
            foreach (var entry in Values)
            {
                float sweepAngle = 360f * entry.Value / Values.Sum(x => x.Value);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 30,
                    Color = entry.HasColour() ? entry.Colour : palette.GetAvaibleColour(),
                };

                //var center = new SKPoint(Width.ToFloat() / 2, Height.ToFloat() / 2);
                var center = new SKPoint(CanvasSize.Width / 2, CanvasSize.Height / 2);
                var slice = new Slice(canvas, paint, CenterRect, center, StartAngle, sweepAngle);
                slice.Draw();

                paint.StrokeWidth = 0;
                DrawLabel(entry, paint);

                StartAngle += sweepAngle;
            }
        }
    }
}
