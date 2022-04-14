using UnityEngine;

namespace Code
{
    public class MathUtilities
    {
        public static bool Approximately(float a, float b, float threshold=0.01f)
        {
            return a >= b - threshold && a <= b + threshold;
        }
    }
}