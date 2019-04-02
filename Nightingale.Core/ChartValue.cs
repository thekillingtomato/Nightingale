﻿using SkiaSharp;

namespace Nightingale.Core
{
    public class ChartValue
    {
        public float Value { get; set; }

        public string Label { get; set; }

        public string Caption { get; set; }

        public SKColor Colour { get; set; }

        public bool IsCaptionEmpty() => string.IsNullOrEmpty(Caption);

        public bool HasColour() => !Colour.Equals(SKColors.Empty);
    }
}
