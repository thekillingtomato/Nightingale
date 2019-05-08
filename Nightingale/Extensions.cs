using System;

namespace Nightingale
{
    public static class Extensions
    {
        public static float ToFloat(this double value) => (float)Convert.ChangeType(value, typeof(float));
    }
}
