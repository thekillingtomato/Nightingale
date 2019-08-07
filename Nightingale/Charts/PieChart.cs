using SkiaSharp;
using System.Linq;

namespace Nightingale.Charts
{
    public class PieChart : CircularChart
    {
        protected override void DrawChart()
        {
            foreach (var entry in Series)
            {
                float sweepAngle = 360f * entry.Value / Series.Sum(x => x.Value);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 30,
                    Color = entry.HasColour() ? entry.Colour : GetDefaultColour(entry),
                };

                var center = new SKPoint(CanvasSize.Width / 2, CanvasSize.Height / 2);

                using (SKPath path = new SKPath())
                {
                    path.MoveTo(center);
                    path.ArcTo(CenterRect, StartAngle, sweepAngle, false);
                    path.Close();
                    paint.Style = SKPaintStyle.Fill;
                    canvas.DrawPath(path, paint);
                }

                paint.StrokeWidth = 0;
                DrawLabel(entry, paint);

                StartAngle += sweepAngle;
            }
        }
    }
}
