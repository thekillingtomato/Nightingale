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

        public PaletteColour()
        {
        }

        public SKColor GetAvaibleColour()
        {
            var firstAvaible = colours.First();
            colours.Remove(firstAvaible);
            return firstAvaible;
        }
    }
}
