using UnityEngine;

namespace Extensions
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float value)
        {
            color.a = value;
            return color;
        }
    }
}