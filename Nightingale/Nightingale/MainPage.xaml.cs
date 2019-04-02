using Nightingale.Core;
using System.Collections.Generic;
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
            barChart.BackgroundColor = Color.FromHex("#202020");
            barChart.Entries = new List<ChartValue>
            {
                new ChartValue
                {
                    Value = 547,
                    Label = "Torta",
                    Caption = "$ 547",
                },
                new ChartValue
                {
                    Value = 40,
                    Label = "Mouse",
                    //Caption = "$ 40",
                },
                new ChartValue
                {
                    Value = 345,
                    Label = "Teclado",
                    Caption = "$ 345",
                },
                new ChartValue
                {
                    Value = 121,
                    Label = "Monitor",
                    Caption = "$ 121",
                },
                new ChartValue
                {
                    Value = 95,
                    Label = "CPU",
                },
                new ChartValue
                {
                    Value = 77,
                    Label = "Variable"
                },
                new ChartValue
                {
                    Value = 780,
                    Label = "Telefono"
                },
                 new ChartValue
                {
                    Value = 455,
                    Label = "Televisor"
                },
                 new ChartValue
                {
                    Value = 589,
                    Label = "Repasador"
                },
                 new ChartValue
                {
                    Value = 215,
                    Label = "XBOX"
                },
                 new ChartValue
                {
                    Value = 365,
                    Label = "PSP"
                },
                 new ChartValue
                {
                    Value = 155,
                    Label = "Auriculares"
                },
                 new ChartValue
                {
                    Value = 280,
                    Label = "Parlantes"
                },
                 new ChartValue
                {
                    Value = 85,
                    Label = "Amplificador"
                },
            };
        }
    }
}
