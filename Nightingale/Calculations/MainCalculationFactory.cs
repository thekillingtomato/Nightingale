using System;
using System.Collections.Generic;
using System.Text;

namespace Nightingale.Calculations
{
    internal class MainCalculationFactory
    {
        protected Chart chart;

        public MainCalculationFactory(Chart chart)
        {
            this.chart = chart;

            MeasureMargins();
            MeasureAxis();
        }

        public float MarginX { get; protected set; }

        public float MarginY { get; protected set; }

        public float AvaibleWidth => chart.CanvasSize.Width - MarginX;

        public float AvaibleHeight => chart.CanvasSize.Height - MarginY;

        public float AxisX { get; protected set; }

        public float AxisY { get; protected set; }

        protected virtual void MeasureMargins()
        {
            MarginY = chart.CanvasSize.Height * 20 / 100;
            MarginX = chart.CanvasSize.Width * 5 / 100;
        }

        protected virtual void MeasureAxis()
        {
            AxisX = chart.HasNegativeValues ?
                        (chart.AllNegatives || chart.AllPositive ? MarginY : AvaibleHeight / 2) :
                        AvaibleHeight;
        }
    }
}
