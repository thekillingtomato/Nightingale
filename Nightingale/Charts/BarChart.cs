using Nightingale.Calculations;
using Nightingale.Drawable;
using Nightingale.Figures;

namespace Nightingale.Charts
{
    public class BarChart : Chart
    {
        internal override MainCalculationFactory CreateFactory() => new BarChartCalculationFactory(this);

        internal override MainDrawableFactory CreateDrawableFactory() => new BarDrawableFactory(calculationFactory as BarChartCalculationFactory, this);

        public override Shape Create(SeriesValue value)
        {
            var factory = calculationFactory as BarChartCalculationFactory;

            var drawableFactory = mainDrawableFactory as BarDrawableFactory;

            var paint = drawableFactory.CreateBarPaint(value);

            var shape = new Bar(canvas)
            {
                Paint = paint,
                Rect = drawableFactory.CreateBarRect(value),
                LabelPosition = drawableFactory.CreateLabelPointPosition(value),
                ValuePosition = drawableFactory.CreateValuePointPosition(value),
                SerieValue = value
            };

            return shape;
        }

        protected override void DrawChart()
        {
            foreach (var shape in Shapes)
            {
                shape.Draw();
            }
        }
    }
}