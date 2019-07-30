using System;

namespace Nightingale.Abstract
{
    public abstract class LinealChart : Chart
    {
        public float IncreaseRatio => MaxEntryValue / AxisY;

        public float AxisY => HasNegativeValues ? avaibleHeight / 2 : avaibleHeight;

        protected float DistanceFromAxisY(ChartValue value)
        {
            var percentage = value.Value / IncreaseRatio * 100 / AxisY;
            var increaseHeight = Math.Abs(percentage * (AxisY - marginY) / 100);
            var result = AxisY - (value.Value > 0 ? increaseHeight : -increaseHeight);
            return result;
        }
    }
}
