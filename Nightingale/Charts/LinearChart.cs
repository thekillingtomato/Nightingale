using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nightingale.Charts
{
    public class LinearChart : Chart
    {
        protected override void DrawChart()
        {            
            foreach (var value in Values)
            {
                var index = Values.IndexOf(value);

                var paint = new SKPaint
                {
                    Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                    TextSize = TextSize,
                    TextAlign = SKTextAlign.Center,
                    StrokeWidth = 20,
                };

                var x = (avaibleWidth / Values.Count) * index + marginX;
                var y = CalculateYHeight(value);

                //if (Values.IndexOf(value) == (Values.Count - 1)) break;
                canvas.DrawPoint(x, y, paint);
                if (index == 0)
                {                    
                    continue;
                }

                if(index % 2 != 0)
                {
                    var prev = Values.ElementAt(index - 1);
                    var x1 = (avaibleWidth / Values.Count) * Values.IndexOf(prev) + marginX;
                    var y1 = CalculateYHeight(prev);
                    //paint.StrokeWidth = 10;
                    paint.Shader = SKShader.CreateLinearGradient(
                        new SKPoint(x, y),
                        new SKPoint(x1, y1),
                        new SKColor[] { value.Colour, prev.Colour },
                        new float[] { 10, 10 },
                        SKShaderTileMode.Repeat);
                    canvas.DrawLine(x, y, x1, y1, paint);
                }
            }
            

            //canvas.DrawPoint(new SKPoint
            //{
            //    X = (avaibleWidth / Values.Count) * Values.IndexOf(value) + marginX,
            //    Y = CalculateYHeight(value)
            //}, paint);

        }

        private float CalculateYHeight(ChartValue value)
        {
            var percentage = value.Value / ValuesRatio * 100 / avaibleHeight;
            var increaseHeight = Math.Abs(percentage * (avaibleHeight - marginY) / 100);
            return avaibleHeight - (value.Value > 0 ? increaseHeight : -increaseHeight);
        }
    }
}
