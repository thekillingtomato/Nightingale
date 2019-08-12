using Nightingale.Calculations;
using Nightingale.Drawable;
using Nightingale.Figures;

namespace Nightingale.Charts
{
    public class PieChart : CircularChart
    {
        internal override MainDrawableFactory CreateDrawableFactory()
        {
            return new PieDrawableFactory(calculationFactory, this);
        }

        public override Shape Create(SeriesValue seriesValue)
        {
            var factory = calculationFactory as PieChartCalculationFactory;
            var drawableFactory = mainDrawableFactory as PieDrawableFactory;

            var paint = drawableFactory.CreateSlicePaint(seriesValue);

            var textPaint = drawableFactory.CreateTextPaint(seriesValue, paint.Color);

            var leftSide = Series.IndexOf(seriesValue) % 2 == 0;

            var text = leftSide ?
                        $"{seriesValue.Label} {(UseCaption() ? seriesValue.Caption : CalculatePercentage(seriesValue).ToString("F0") + "%")}" :
                        $"{(UseCaption() ? seriesValue.Caption : CalculatePercentage(seriesValue).ToString("F0") + "%")} {seriesValue.Label}";

            return new Slice(canvas)
            {
                SerieValue = seriesValue,
                CenterRect = CenterRect,
                Center = CenterPoint,
                Paint = paint,
                LabelPosition = drawableFactory.CreateLabelPoint(seriesValue, textPaint, text),
                StartAngle = factory.CalculateStartAngle(),
                SweepAngle = factory.CalculateSweepAngle(seriesValue),
                Text = text,
                TextPaint = textPaint
            };
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