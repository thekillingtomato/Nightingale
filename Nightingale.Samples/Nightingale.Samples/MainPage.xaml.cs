using SkiaSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nightingale
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (Device.Idiom.Equals(TargetIdiom.Phone))
            {
                barChart.TextSize = 24;
                pieChart.TextSize = doughnutChart.TextSize = 34;
                pieChart.HeightRequest = doughnutChart.HeightRequest = 150;
            }

            BindingContext = new MainViewModel();
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1500);

                Init();
            });
        }

        public List<SeriesValue> Entries { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Init()
        {
            Entries = new List<SeriesValue>
            {
                new SeriesValue
                {
                    Value = 580,
                    Label = "Torta",
                    //Caption = "$ 580",
                    Colour = SKColors.CadetBlue
                },
                new SeriesValue
                {
                    Value = 5000,
                    Label = "Mouse",
                    //Caption = "$ 463",
                    Colour = SKColors.Pink
                },
                new SeriesValue
                {
                    Value = 240,
                    Label = "Teclado",
                    //Caption = "$ 240",
                    Colour = SKColors.Black
                },
                new SeriesValue
                {
                    Value = 3600,
                    Label = "Monitor",
                    Caption = "$ 3600",
                    Colour = SKColors.Brown
                },
                new SeriesValue
                {
                    Value = 1700,
                    Label = "CPU",
                    Caption = "$ 1700",
                    Colour = SKColors.White
                },
                new SeriesValue
                {
                    Value = 1200,
                    Label = "Variable",
                    Caption = "$ 1200",
                    Colour = SKColors.Yellow
                },
                new SeriesValue
                {
                    Value = 5000,
                    Label = "Telefono",
                    Caption = "$ 3500",
                    Colour = SKColors.Green
                },
                 new SeriesValue
                {
                    Value = -5000,
                    Label = "Televisor",
                    Caption = "$ 5000",
                    Colour = SKColors.Violet
                },
                 new SeriesValue
                {
                    Value = -60,
                    Label = "Repasador",
                    Caption = "$ 60",
                    Colour = SKColors.Blue
                },
                 new SeriesValue
                {
                    Value = -450,
                    Label = "XBOX",
                    Caption = "$ 450",
                    Colour = SKColors.Red
                }
            };

            Entries.ForEach(e => e.Colour = SKColor.Empty);
            //Entries.ForEach(e => e.Value = -e.Value);
            Entries = Entries.Where(x => x.Value >= 0).ToList();
            //Entries = Entries.Where(x => x.Value < 0).ToList();
            //Entries = Entries.Take(3).ToList();

            OnPropertyChanged(nameof(Entries));
        }
    }
}
