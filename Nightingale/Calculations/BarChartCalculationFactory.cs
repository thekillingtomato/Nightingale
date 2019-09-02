namespace Nightingale.Calculations
{
    internal abstract class BarChartCalculationFactory : MainCalculationFactory
    {
        public BarChartCalculationFactory(Chart chart) 
            : base(chart)
        {
            MeasureBarSizeAndDistance();
        }

        protected override void MeasureMargins()
        {
            base.MeasureMargins();
            MarginY = chart.CanvasSize.Height * (chart.AllNegatives || chart.AllPositive ? 25 : 15) / 100;
        }

        protected abstract void MeasureBarSizeAndDistance();

        public float SpaceBetweenBars { get; protected set; }

        public float BarSize { get; protected set; }

        public float CalculateBarStartingPoint(int index)
        {
            return (index * BarSize + SpaceBetweenBars) * 2;
        }

        public virtual float GetLabelPointY () => chart.CanvasSize.Height - 40;

        public virtual float GetLabelValuePointY() => chart.CanvasSize.Height - 10;

        public abstract float CalculateLeft(float barStartingPoint);

        public abstract float CalculateRight(float barStartingPoint);

        public abstract float CalculateRight(SeriesValue value);

        public abstract float CalculateBottom();

        public abstract float CalculateBottom(float barStartingPoint);

        public abstract float CalculateTop(float barStartingPoint);

        public abstract float CalculateTop(SeriesValue value);
    }
}
