using Nightingale.Core;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Nightingale
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            var entries = new List<ChartValue>
            {
                new ChartValue
                {
                    Value = 580,
                    Label = "Torta",
                    Caption = "$ 580",
                    Colour = SKColors.CadetBlue
                },
                new ChartValue
                {
                    Value = 463,
                    Label = "Mouse",
                    Caption = "$ 463",
                    Colour = SKColors.Pink
                },
                new ChartValue
                {
                    Value = 240,
                    Label = "Teclado",
                    Caption = "$ 240",
                    Colour = SKColors.Black
                },
                new ChartValue
                {
                    Value = 3600,
                    Label = "Monitor",
                    Caption = "$ 3600",
                    Colour = SKColors.Brown
                },
                new ChartValue
                {
                    Value = 1700,
                    Label = "CPU",
                    Caption = "$ 1700",
                    Colour = SKColors.White
                },
                new ChartValue
                {
                    Value = 1200,
                    Label = "Variable",
                    Caption = "$ 1200",
                    Colour = SKColors.Yellow
                },
                new ChartValue
                {
                    Value = 3500,
                    Label = "Telefono",
                    Caption = "$ 3500",
                    Colour = SKColors.Green
                },
                 new ChartValue
                {
                    Value = 5000,
                    Label = "Televisor",
                    Caption = "$ 5000",
                    Colour = SKColors.Violet
                },
                 new ChartValue
                {
                    Value = 60,
                    Label = "Repasador",
                    Caption = "$ 60",
                    Colour = SKColors.Blue
                },
                 new ChartValue
                {
                    Value = 450,
                    Label = "XBOX",
                    Caption = "$ 450",
                    Colour = SKColors.Red
                },
                // new ChartValue
                //{
                //    Value = 13,
                //    Label = "PSP"
                //},
                // new ChartValue
                //{
                //    Value = -40,
                //    Label = "Auriculares"
                //},
                // new ChartValue
                //{
                //    Value = 18,
                //    Label = "Parlantes"
                //},
                // new ChartValue
                //{
                //    Value = -26,
                //    Label = "Amplificador"
                //},
            };

            var all = entries.Sum(x => x.Value);

            pieChart.BackgroundColor = Color.FromHex("#202020");
            pieChart.Values = entries;

            barChart.BackgroundColor = Color.FromHex("#202020");
            barChart.Values = entries;

            doughnutChart.BackgroundColor = Color.FromHex("#202020");
            doughnutChart.Values = entries;
        }
    }
}
