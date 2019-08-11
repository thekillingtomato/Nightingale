using Nightingale.Calculations;
using SkiaSharp;
using System.Linq;

namespace Nightingale
{
    public abstract class CircularChart : Chart
    {
        internal override MainCalculationFactory CreateFactory() => new PieChartCalculationFactory(this);

        public SKPoint CenterPoint
        {
            get
            {                
                var factory = calculationFactory as PieChartCalculationFactory;
                return new SKPoint(factory.CenterX, factory.CenterY);
            }
        }

        public SKRect CenterRect
        {
            get
            {
                var factory = calculationFactory as PieChartCalculationFactory;

                return new SKRect(CenterPoint.X - factory.Radius, CenterPoint.Y - factory.Radius,
                                                    CenterPoint.X + factory.Radius, CenterPoint.Y + factory.Radius);
            }
        }

        protected float CalculatePercentage(SeriesValue value) => value.Value * 100 / Series.Sum(x => x.Value);
    }
}
