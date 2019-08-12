using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace Nightingale
{
    public class PaletteColour
    {
        private List<SKColor> colours = new List<SKColor>
        {
            SKColors.Aquamarine,
            SKColors.BurlyWood,
            SKColors.CornflowerBlue,
            SKColors.Coral,
            SKColors.GreenYellow,
            SKColors.HotPink,
            SKColors.Lavender,
            SKColors.LightBlue,
            SKColors.LightCoral,
            SKColors.LightGreen,
            SKColors.LightSalmon,
            SKColors.LightSkyBlue,
            SKColors.MediumSpringGreen,
            SKColors.MediumOrchid,
            SKColors.MintCream,
            SKColors.PaleGoldenrod,
            SKColors.Orchid,
            SKColors.PowderBlue,
            SKColors.Plum,
            SKColors.Peru,
            SKColors.SandyBrown
        };

        private List<SKColor> usedColours = new List<SKColor>();

        public SKColor GetAvaibleColour()
        {
            var availableColors = colours.Except(usedColours);
            var firstAvaibleColor = availableColors.FirstOrDefault();
            usedColours.Add(firstAvaibleColor);
            return firstAvaibleColor;
        }

        public IEnumerable<SKColor> GetColours(int take) => colours.Take(take);
    }
}
