using SkiaSharp;
using System.Linq;

namespace Nightingale.Charts
{
    public class DoughnutChart : CircularChart
    {
        protected override void DrawChart()
        {
            foreach (var value in Values)
            {
                float sweepAngle = 360f * value.Value / Values.Sum(x => x.Value);

                var colour = value.HasColour() ? value.Colour : GetDefaultColour(value);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 40,
                    Color = colour,
                };

                using (SKPath path = new SKPath())
                {
                    path.AddArc(CenterRect, StartAngle, sweepAngle);
                    canvas.DrawPath(path, paint);
                }

                DrawLabel(value, new SKPaint { Color = colour });

                StartAngle += sweepAngle;
            }
        }
    }
}
