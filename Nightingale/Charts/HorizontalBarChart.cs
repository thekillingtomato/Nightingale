using Nightingale.Calculations;

namespace Nightingale.Charts
{
    public class HorizontalBarChart : BarChart
    {
        internal override MainCalculationFactory CreateFactory() => new HorizontalBarChartCalculationFactory(this);
    }
}
