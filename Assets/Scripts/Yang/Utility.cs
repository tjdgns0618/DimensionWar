using UnityEngine;

namespace PI
{
    public static class Utility
    {
        //¿¹½Ã
        //public static void Shuffle()
        //{

        //}

        public static float linear(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value);
        }
    }
}