using Nightingale.Calculations;

namespace Nightingale.Charts
{
    public class VerticalBarChart : BarChart
    {
        internal override MainCalculationFactory CreateFactory() => new VerticalBarChartCalculationFactory(this);
    }
}
