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
            var entries = new List<ChartValue>
            {
                new ChartValue
                {
                    Value = - 88,
                    Label = "Torta",
                    Caption = "$ 547",
                },
                new ChartValue
                {
                    Value = 43,
                    Label = "Mouse",
                    //Caption = "$ 40",
                },
                new ChartValue
                {
                    Value = 24,
                    Label = "Teclado",
                    Caption = "$ 345",
                },
                new ChartValue
                {
                    Value = 36,
                    Label = "Monitor",
                    Caption = "$ 121",
                },
                new ChartValue
                {
                    Value = 29,
                    Label = "CPU",
                },
                new ChartValue
                {
                    Value = 12,
                    Label = "Variable"
                },
                new ChartValue
                {
                    Value = 40,
                    Label = "Telefono"
                },
                 new ChartValue
                {
                    Value = 19,
                    Label = "Televisor"
                },
                 new ChartValue
                {
                    Value = 31,
                    Label = "Repasador"
                },
                 new ChartValue
                {
                    Value = 36,
                    Label = "XBOX"
                },
                 new ChartValue
                {
                    Value = 13,
                    Label = "PSP"
                },
                 new ChartValue
                {
                    Value = -40,
                    Label = "Auriculares"
                },
                 new ChartValue
                {
                    Value = 18,
                    Label = "Parlantes"
                },
                 new ChartValue
                {
                    Value = -26,
                    Label = "Amplificador"
                },
            };

            pieChart.BackgroundColor = Color.FromHex("#202020");
            pieChart.Entries = entries;

            barChart.BackgroundColor = Color.FromHex("#202020");
            barChart.Entries = entries;
        }
    }
}
