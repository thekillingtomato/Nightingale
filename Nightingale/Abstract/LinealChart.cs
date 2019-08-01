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
    }
}
