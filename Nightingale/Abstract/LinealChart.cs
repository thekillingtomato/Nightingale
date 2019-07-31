using System;

namespace Nightingale.Abstract
{
    public abstract class LinealChart : Chart
    {
        public float IncreaseRatio => MaxEntryValue / AxisY;

        public float AxisY => HasNegativeValues ? avaibleHeight / 2 : avaibleHeight;

        protected virtual float DistanceFromAxisY(ChartValue value)
        {
            var percentage = value.Value / IncreaseRatio * 100 / AxisY;
            var increaseHeight = Math.Abs(percentage * (AxisY - marginY) / 100);
            var result = AxisY - (value.Value > 0 ? increaseHeight : -increaseHeight);
            return result;
        }

        protected override void MeasureMargins()
        {
            base.MeasureMargins();
            marginY = CanvasSize.Height * (HasNegativeValues ? 15 : 25) / 100;
        }
    }
}
