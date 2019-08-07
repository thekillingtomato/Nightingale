using System;
using System.Collections.Generic;
using System.Linq;

namespace Nightingale
{
    public static class Extensions
    {
        public static float ToFloat(this double value) => (float)Convert.ChangeType(value, typeof(float));

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();

        public static bool NotNullNorEmpty<T>(this IEnumerable<T> collection) => !IsNullOrEmpty(collection);

        public static byte ChangeBy(this byte value, int percentage)
        {
            return (byte)(value > 127 ?
                value - (255 * 10 / 100):
                value + (255 * 10 / 100));
        }
    }
}
