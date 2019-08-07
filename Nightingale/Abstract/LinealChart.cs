using SkiaSharp;
using System;

namespace Nightingale.Abstract
{
    public abstract class LinealChart : Chart
    {
        public float IncreaseRatio => MaxEntryValue / AxisX;

        public float AxisX => HasNegativeValues ?
            (AllNegatives || AllPositive ? marginY : avaibleHeight / 2) :
            avaibleHeight;

        protected virtual float DistanceFromAxisX(ChartValue value)
        {
            var percentage = value.Value / IncreaseRatio * 100 / AxisX;
            var increaseHeight = Math.Abs(percentage * ((AllNegatives ? -avaibleHeight : -marginY) + AxisX) / 100);
            var result = AxisX - (value.Value > 0 ? increaseHeight : -increaseHeight);
            return result;
        }

        protected override void MeasureMargins()
        {
            base.MeasureMargins();
            marginY = CanvasSize.Height * (AllNegatives || AllPositive ? 25 : 15) / 100;
        }

        protected virtual SKPaint CreateDotPaint(ChartValue value)
        {
            return new SKPaint
            {
                Color = !value.Colour.Equals(SKColor.Empty) ? value.Colour : GetDefaultColour(value),
                StrokeWidth = 20,
                TextSize = TextSize,
                TextAlign = SKTextAlign.Center
            };
        }

        protected virtual SKPoint CreatePoint(ChartValue value)
        {
            var x = (avaibleWidth / Values.Count) * Values.IndexOf(value) + marginX;
            var y = DistanceFromAxisX(value);

            return new SKPoint(x, y);
        }
    }
}
