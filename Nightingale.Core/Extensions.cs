using System;

namespace Nightingale.Core
{
    public static class Extensions
    {
        public static float ToFloat(this double value) => (float)Convert.ChangeType(value, typeof(float));
    }
}
