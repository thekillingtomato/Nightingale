using Nightingale.Core.Shapes;
using SkiaSharp;
using System.Linq;

namespace Nightingale.Core.Charts
{
    public class DoughnutChart : CircularChart
    {
        protected override void DrawChart()
        {
            foreach (var value in Values)
            {
                float sweepAngle = 360f * value.Value / Values.Sum(x => x.Value);

                var colour = value.HasColour() ? value.Colour : palette.GetAvaibleColour();

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 40,
                    Color = colour,
                };

                var arc = new Arc(canvas, paint, CenterRect, StartAngle, sweepAngle);
                arc.Draw();
                
                DrawLabel(value, new SKPaint { Color = colour });

                StartAngle += sweepAngle;
            }
        }
    }
}
