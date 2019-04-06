using Nightingale.Core.Shapes;
using SkiaSharp;
using System.Linq;

namespace Nightingale.Core.Charts
{
    public class DoughnutChart : CircularChart
    {
        protected override void DrawChart()
        {
            foreach (var entry in Entries)
            {
                float sweepAngle = 360f * entry.Value / Entries.Sum(x => x.Value);

                var colour = entry.HasColour() ? entry.Colour : palette.GetAvaibleColour();

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 30,
                    Color = colour,
                };

                var arc = new Arc(canvas, paint, CenterRect, StartAngle, sweepAngle);
                arc.Draw();
                
                DrawLabel(entry, new SKPaint { Color = colour });

                StartAngle += sweepAngle;
            }
        }
    }
}
