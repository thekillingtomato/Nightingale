using Nightingale.Calculations;

namespace Nightingale.Drawable
{
    internal class MainDrawableFactory
    {
        protected readonly MainCalculationFactory factory;
        protected readonly Chart chart;
        protected PaletteColour palette = new PaletteColour();

        internal MainDrawableFactory(MainCalculationFactory factory, Chart chart)
        {
            this.factory = factory;
            this.chart = chart;
        }
    }
}
