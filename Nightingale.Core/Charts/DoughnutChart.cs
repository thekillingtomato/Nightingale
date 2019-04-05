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
                var yPoint = CanvasSize.Height - (CanvasSize.Height - 10) + (Entries.IndexOf(entry) + 1) * 20;
                var xPoint = CanvasSize.Width - avaibleWidth;

                var labelPosition = new SKPoint(xPoint, yPoint);

                float sweepAngle = 360f * entry.Value / Entries.Sum(x => x.Value);

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 30,
                    Color = entry.HasColour() ? entry.Colour : palette.GetAvaibleColour(),
                };

                var slice = new Slice(canvas, paint, CenterRect, StartAngle, sweepAngle);
                slice.Draw();
                
                paint.StrokeWidth = 0;
                var text = $"{entry.Label} - {(entry.IsCaptionEmpty() ? entry.Value.ToString() : entry.Caption)}";
                var reference = new ReferenceLabel(canvas, labelPosition, paint, text);
                reference.Draw();

                StartAngle += sweepAngle;
            }
        }
    }
}
